using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _camera;
    private Transform _transform;
    private Vector3 _lookAt = Vector3.zero;

    private void Start()
    {
        _camera = Camera.main.transform;
        _transform = transform;
    }

    private void LateUpdate()
    {
        _lookAt.x = _transform.position.x;
        _lookAt.y = _camera.position.y;
        _lookAt.z = _camera.position.z;

        _transform.LookAt(_lookAt);
    }
}
