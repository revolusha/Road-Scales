using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Water
{
    [ExecuteInEditMode]
    public class Water : MonoBehaviour
    {
        public enum WaterMode
        {
            Simple = 0,
            Reflective = 1,
            Refractive = 2,
        };

        [SerializeField] private WaterMode _waterMode = WaterMode.Refractive;
        [SerializeField] private bool _disablePixelLights = true;
        [SerializeField] private int _textureSize = 256;
        [SerializeField] private float _clipPlaneOffset = 0.07f;
        [SerializeField] private LayerMask _reflectLayers = -1;
        [SerializeField] private LayerMask _refractLayers = -1;

        private const string WaveScale = "_WaveScale";
        private const string WaveSpeed = "WaveSpeed";
        private const string WaveOffset = "_WaveOffset";
        private const string WaveScale4 = "_WaveScale4";
        private const string KeyWordSimple = "WATER_SIMPLE";
        private const string KeyWordReflective = "WATER_REFLECTIVE";
        private const string KeyWordRefractive = "WATER_REFRACTIVE";

        private static bool _isInsideWater;

        private Dictionary<Camera, Camera> _reflectionCameras = new();
        private Dictionary<Camera, Camera> _refractionCameras = new();

        private int _oldReflectionTextureSize;
        private int _oldRefractionTextureSize;
        private RenderTexture _reflectionTexture;
        private RenderTexture _refractionTexture;
        private WaterMode _hardwareWaterSupport = WaterMode.Refractive;

        private void OnDisable()
        {
            DisableMode(ref _reflectionTexture, _reflectionCameras);
            DisableMode(ref _refractionTexture, _refractionCameras);
        }

        private void Update()
        {
            if (GetComponent<Renderer>() == null)
                return;

            Material material = GetComponent<Renderer>().sharedMaterial;

            if (material == null)
                return;

            float waveScale = material.GetFloat(WaveScale);
            double t = Time.timeSinceLevelLoad / 20.0;
            Vector4 waveScale4 = new(waveScale, waveScale, waveScale * 0.4f, waveScale * 0.45f);
            Vector4 waveSpeed = material.GetVector(WaveSpeed);
            Vector4 offsetClamped = new(
                (float)Math.IEEERemainder(waveSpeed.x * waveScale4.x * t, 1.0),
                (float)Math.IEEERemainder(waveSpeed.y * waveScale4.y * t, 1.0),
                (float)Math.IEEERemainder(waveSpeed.z * waveScale4.z * t, 1.0),
                (float)Math.IEEERemainder(waveSpeed.w * waveScale4.w * t, 1.0)
                );

            material.SetVector(WaveOffset, offsetClamped);
            material.SetVector(WaveScale4, waveScale4);
        }

        public void OnWillRenderObject()
        {

            if (enabled == false ||
                GetComponent<Renderer>() == null ||
                GetComponent<Renderer>().sharedMaterial == null ||
                GetComponent<Renderer>().enabled == false)
                return;

            Camera cam = Camera.current;

            if (cam == null || _isInsideWater)
                return;

            int oldPixelLightCount = QualitySettings.pixelLightCount;
            Vector3 pos = transform.position;
            Vector3 normal = transform.up;
            WaterMode mode = GetWaterMode();

            if (_disablePixelLights)
                QualitySettings.pixelLightCount = 0;

            _isInsideWater = true;
            _hardwareWaterSupport = FindHardwareWaterSupport();
            CreateWaterObjects(cam, out Camera reflectionCamera, out Camera refractionCamera);
            UpdateCameraModes(cam, reflectionCamera);
            UpdateCameraModes(cam, refractionCamera);

            if (mode >= WaterMode.Reflective)
            {
                float d = -Vector3.Dot(normal, pos) - _clipPlaneOffset;
                Vector4 reflectionPlane = new(normal.x, normal.y, normal.z, d);

                Matrix4x4 reflection = Matrix4x4.zero;
                CalculateReflectionMatrix(ref reflection, reflectionPlane);
                Vector3 oldpos = cam.transform.position;
                Vector3 newpos = reflection.MultiplyPoint(oldpos);
                reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

                Vector4 clipPlane = CameraSpacePlane(reflectionCamera, pos, normal, 1.0f);
                reflectionCamera.projectionMatrix = cam.CalculateObliqueMatrix(clipPlane);

                reflectionCamera.cullingMatrix = cam.projectionMatrix * cam.worldToCameraMatrix;

                reflectionCamera.cullingMask = ~(1 << 4) & _reflectLayers.value;
                reflectionCamera.targetTexture = _reflectionTexture;
                bool oldCulling = GL.invertCulling;
                GL.invertCulling = !oldCulling;
                reflectionCamera.transform.position = newpos;
                Vector3 euler = cam.transform.eulerAngles;
                reflectionCamera.transform.eulerAngles = new Vector3(-euler.x, euler.y, euler.z);
                reflectionCamera.Render();
                reflectionCamera.transform.position = oldpos;
                GL.invertCulling = oldCulling;
                GetComponent<Renderer>().sharedMaterial.SetTexture("_ReflectionTex", _reflectionTexture);
            }

            if (mode >= WaterMode.Refractive)
            {
                refractionCamera.worldToCameraMatrix = cam.worldToCameraMatrix;

                Vector4 clipPlane = CameraSpacePlane(refractionCamera, pos, normal, -1.0f);
                refractionCamera.projectionMatrix = cam.CalculateObliqueMatrix(clipPlane);

                refractionCamera.cullingMatrix = cam.projectionMatrix * cam.worldToCameraMatrix;

                refractionCamera.cullingMask = ~(1 << 4) & _refractLayers.value;
                refractionCamera.targetTexture = _refractionTexture;
                refractionCamera.transform.SetPositionAndRotation(cam.transform.position, cam.transform.rotation);
                refractionCamera.Render();
                GetComponent<Renderer>().sharedMaterial.SetTexture("_RefractionTex", _refractionTexture);
            }

            if (_disablePixelLights)
                QualitySettings.pixelLightCount = oldPixelLightCount;

            switch (mode)
            {
                case WaterMode.Simple:
                    SetActiveKeyWords(true, false, false);
                    break;

                case WaterMode.Reflective:
                    SetActiveKeyWords(false, true, false);
                    break;

                case WaterMode.Refractive:
                    SetActiveKeyWords(false, false, true);
                    break;
            }

            _isInsideWater = false;
        }

        private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
        {
            reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
            reflectionMat.m01 = (-2F * plane[0] * plane[1]);
            reflectionMat.m02 = (-2F * plane[0] * plane[2]);
            reflectionMat.m03 = (-2F * plane[3] * plane[0]);

            reflectionMat.m10 = (-2F * plane[1] * plane[0]);
            reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
            reflectionMat.m12 = (-2F * plane[1] * plane[2]);
            reflectionMat.m13 = (-2F * plane[3] * plane[1]);

            reflectionMat.m20 = (-2F * plane[2] * plane[0]);
            reflectionMat.m21 = (-2F * plane[2] * plane[1]);
            reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
            reflectionMat.m23 = (-2F * plane[3] * plane[2]);

            reflectionMat.m30 = 0F;
            reflectionMat.m31 = 0F;
            reflectionMat.m32 = 0F;
            reflectionMat.m33 = 1F;
        }

        private void UpdateCameraModes(Camera sourceCamera, Camera destinatedCamera)
        {
            if (destinatedCamera == null)
                return;

            destinatedCamera.clearFlags = sourceCamera.clearFlags;
            destinatedCamera.backgroundColor = sourceCamera.backgroundColor;

            if (sourceCamera.clearFlags == CameraClearFlags.Skybox)
            {
                Skybox sky = sourceCamera.GetComponent<Skybox>();
                Skybox mysky = destinatedCamera.GetComponent<Skybox>();

                if (sky == null || sky.material == null)
                    mysky.enabled = false;
                else
                {
                    mysky.enabled = true;
                    mysky.material = sky.material;
                }
            }

            destinatedCamera.farClipPlane = sourceCamera.farClipPlane;
            destinatedCamera.nearClipPlane = sourceCamera.nearClipPlane;
            destinatedCamera.orthographic = sourceCamera.orthographic;
            destinatedCamera.fieldOfView = sourceCamera.fieldOfView;
            destinatedCamera.aspect = sourceCamera.aspect;
            destinatedCamera.orthographicSize = sourceCamera.orthographicSize;
        }

        private void CreateWaterObjects(Camera currentCamera, out Camera reflectionCamera, out Camera refractionCamera)
        {
            WaterMode mode = GetWaterMode();

            reflectionCamera = null;
            refractionCamera = null;

            if (mode >= WaterMode.Reflective)
                HandleWaterObjectsCreationMode(ref _reflectionTexture, ref _oldReflectionTextureSize,
                    currentCamera, ref reflectionCamera,
                    ref _reflectionCameras, nameof(reflectionCamera));

            if (mode >= WaterMode.Refractive)
                HandleWaterObjectsCreationMode(ref _refractionTexture, ref _oldRefractionTextureSize,
                    currentCamera, ref refractionCamera,
                    ref _refractionCameras, nameof(refractionCamera));
        }

        private void HandleWaterObjectsCreationMode(ref RenderTexture texture, ref int oldTextureSize,
            Camera currentCamera, ref Camera modeCamera,
            ref Dictionary<Camera, Camera> camerasDictionary, string cameraName = "camera")
        {
            if (texture == null || _oldReflectionTextureSize != _textureSize)
            {
                if (texture)
                    DestroyImmediate(texture);

                texture = new(_textureSize, _textureSize, 16);
                texture.name = "__WaterReflection" + GetInstanceID();
                texture.isPowerOfTwo = true;
                texture.hideFlags = HideFlags.DontSave;
                oldTextureSize = _textureSize;
            }

            camerasDictionary.TryGetValue(currentCamera, out modeCamera);
            if (modeCamera == null)
            {
                GameObject go = new("Water " + cameraName + " id" + GetInstanceID() + " for " + currentCamera.GetInstanceID(),
                    typeof(Camera), typeof(Skybox));
                modeCamera = go.GetComponent<Camera>();
                modeCamera.enabled = false;
                modeCamera.transform.SetPositionAndRotation(transform.position, transform.rotation);
                modeCamera.gameObject.AddComponent<FlareLayer>();
                go.hideFlags = HideFlags.HideAndDontSave;
                camerasDictionary[currentCamera] = modeCamera;
            }
        }

        private void DisableMode(ref RenderTexture renderTexture, Dictionary<Camera, Camera> cameras)
        {
            if (renderTexture)
            {
                DestroyImmediate(renderTexture);
                renderTexture = null;
            }

            foreach (var keyValuePair in cameras)
                DestroyImmediate((keyValuePair.Value).gameObject);

            cameras.Clear();
        }

        private void SetActiveKeyWords(bool isSimple, bool isReflective, bool isRefractive)
        {
            if (isSimple)
                Shader.EnableKeyword(KeyWordSimple);
            else
                Shader.DisableKeyword(KeyWordSimple);

            if (isReflective)
                Shader.EnableKeyword(KeyWordReflective);
            else
                Shader.DisableKeyword(KeyWordReflective);

            if (isRefractive)
                Shader.EnableKeyword(KeyWordRefractive);
            else
                Shader.DisableKeyword(KeyWordRefractive);

        }

        private WaterMode GetWaterMode()
        {
            if (_hardwareWaterSupport < _waterMode)
                return _hardwareWaterSupport;

            return _waterMode;
        }

        private WaterMode FindHardwareWaterSupport()
        {
            const string WaterModeString = "WATERMODE";
            const string Refractive = nameof(Refractive);
            const string Reflective = nameof(Reflective);

            if (GetComponent<Renderer>() == null)
                return WaterMode.Simple;

            Material mat = GetComponent<Renderer>().sharedMaterial;

            if (mat == null)
                return WaterMode.Simple;

            string mode = mat.GetTag(WaterModeString, false);

            if (mode == Refractive)
                return WaterMode.Refractive;

            if (mode == Reflective)
                return WaterMode.Reflective;

            return WaterMode.Simple;
        }

        private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
        {
            Vector3 offsetPos = pos + normal * _clipPlaneOffset;
            Matrix4x4 m = cam.worldToCameraMatrix;
            Vector3 cpos = m.MultiplyPoint(offsetPos);
            Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
            return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
        }
    }
}