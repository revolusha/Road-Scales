using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTCameraChanging : MonoBehaviour
{
    [SerializeField] private Camera[] _cameras;

    private Camera _currentCamera;
    private int _index;

    private int NextIndex 
    {
        get
        {
            _index += 1;

            if (_index >= _cameras.Length)
                _index = 0;

            return _index;
        }
    }

    private void OnEnable()
    {
        _index = 0;
        _currentCamera = _cameras[_index];

        for (int i = 1; i < _cameras.Length; i++)
            _cameras[i].enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            ChangeCamera();
    }

    private void ChangeCamera()
    {
        _cameras[NextIndex].enabled = true;
        _currentCamera.enabled = false;
        _currentCamera = _cameras[_index];
    }
}
