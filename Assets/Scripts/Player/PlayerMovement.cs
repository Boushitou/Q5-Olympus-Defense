using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;

    private int _movementSpeed = 30;
    private int _scrollSpeed = 2;
    private Vector3 _direction = Vector3.zero;

    private float _xDir = 0;
    private float _yDir = 0;
    private float _zDir = 0;

    private void Awake()
    {
        _transform = transform;
    }

    /*private void Update()
    {
        Move();
    }

    private void Move()
    {
        _direction.x = _transform.right.x   * _xDir;
        _direction.y = _transform.forward.y * _yDir;
        _direction.z = _transform.forward.z * _zDir;

        _transform.position += _direction * (_speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        _xDir = direction.x;
        _yDir = direction.y;
        _zDir = direction.z;
    }*/

    public void MoveForward()
    {
        _direction = Vector3.zero;
        _direction.z = _transform.forward.z;

        _transform.position += _direction * (_movementSpeed * Time.deltaTime);
    }

    public void MoveBackward()
    {
        _direction = Vector3.zero;
        _direction.z = _transform.forward.z;

        _transform.position -= _direction * (_movementSpeed * Time.deltaTime);
    }

    public void MoveRight()
    {
        _direction = Vector3.zero;
        _direction.x = _transform.right.x;

        _transform.position += _direction * (_movementSpeed * Time.deltaTime);
    }

    public void MoveLeft()
    {
        _direction = Vector3.zero;
        _direction.x = _transform.right.x;

        _transform.position -= _direction * (_movementSpeed * Time.deltaTime);
    }

    public void MoveZoom(float direction)
    {
        _transform.position += _transform.forward * (direction * _scrollSpeed);
    }
}
