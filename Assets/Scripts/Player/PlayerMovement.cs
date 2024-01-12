using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;

    private int _movementSpeed = 30;
    [SerializeField] private int _zoomSpeed = 30;
    private Vector3 _direction = Vector3.zero;
    private Vector3 _position = Vector3.zero;

    private float _limitMinX = 0f;
    private float _limitMaxX = 90f;

    private float _limitMinY = 10f;
    private float _limitMaxY = 100f;

    private float _limitMinZ = -2f;
    private float _limitMaxZ = 90f;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        CheckMove();

        _transform.position += _direction * (_movementSpeed * Time.deltaTime);

        CheckPosition();
    }

    private void CheckMove()
    {
        if (_direction.x > 0 && _transform.position.x >= _limitMaxX)
        {
            _direction.x = 0;
        }
        else if (_direction.x < 0 && _transform.position.x <= _limitMinX)
        {
            _direction.x = 0;
        }

        if (_direction.z > 0 && _transform.position.z >= _limitMaxZ)
        {
            _direction.z = 0;
        }
        else if (_direction.z < 0 && _transform.position.z <= _limitMinZ)
        {
            _direction.z = 0;
        }
    }

    private void CheckPosition()
    {
        _position = _transform.position;

        if (_transform.position.x > _limitMaxX)
        {
            _position.x = _limitMaxX;
            _transform.position = _position;
        }
        else if (_transform.position.x < _limitMinX)
        {
            _position.x = _limitMinX;
            _transform.position = _position;
        }

        if (_transform.position.y > _limitMaxY)
        {
            _position.y = _limitMaxY;
            _transform.position = _position;
        }
        else if (_transform.position.y < _limitMinY)
        {
            _position.y = _limitMinY;
            _transform.position = _position;
        }

        if (_transform.position.z > _limitMaxZ)
        {
            _position.z = _limitMaxZ;
            _transform.position = _position;
        }
        else if (_transform.position.z < _limitMinZ)
        {
            _position.z = _limitMinZ;
            _transform.position = _position;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction.x = direction.x;
        _direction.z = direction.y;
    }

    public void SetZoom(float zoom)
    {
        if (zoom > 0 && _transform.position.y >= _limitMaxY)
        {
            _direction.y = 0;
        }
        else if (zoom < 0 && _transform.position.y <= _limitMinY)
        {
            _direction.y = 0;
        }
        else
        {
            _direction.y = zoom * _zoomSpeed;
        }
    }
}
