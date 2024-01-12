using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;

    private int _movementSpeed = 30;
    [SerializeField] private int _zoomSpeed = 30;
    private Vector3 _direction = Vector3.zero;

    private float _limitX = 90f;
    private float _limitZ = 50f;

    private float _limitMinY = 10f;
    private float _limitMaxY = 100f;

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
        if (_direction != Vector3.zero)
        {
            _transform.position += _direction * (_movementSpeed * Time.deltaTime);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction.x =  direction.x;
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
