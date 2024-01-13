using UnityEngine;

public class Arrow : Projectile
{
    private void Start()
    {
        _speed = 50f;
    }

    public override void Movement()
    {
        if (_enemy != null)
        {
            _direction = (_enemy.GetComponent<Collider>().bounds.center - _myTransform.position).normalized;
        }

        _myTransform.position += _direction * _speed * Time.deltaTime;
        _myTransform.rotation = Quaternion.LookRotation(_direction);
    }
}
