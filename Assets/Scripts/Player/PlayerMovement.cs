using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;

    private int _movementSpeed = 30;
    private Vector3 _direction = Vector3.zero;


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
        _transform.position += _direction * (_movementSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction.x = direction.x;
        _direction.z = direction.y;
    }

    public void SetZoom(float zoom)
    {
        _direction.y = -zoom;
    }
}
