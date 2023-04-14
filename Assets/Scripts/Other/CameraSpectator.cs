using UnityEngine;

public class CameraSpectator : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.current;
    }

    public void LookAtCamera()
    {
        transform.LookAt( _camera.transform.position );
    }
}