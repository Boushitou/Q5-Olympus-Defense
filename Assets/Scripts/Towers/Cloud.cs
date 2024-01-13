using UnityEngine;

public class Cloud : Projectile
{
    private Vector3 _enemyLastPos;
    private Vector3 _destinationPos;

    private float _duration = 3f;
    private float _time = 0f;

    private void Start()
    {
        _speed = 10f;

        _enemyLastPos = _enemy.transform.position;
        _destinationPos = new Vector3(_enemy.transform.position.x, _myTransform.position.y, _enemy.transform.position.z);

        _time = Time.time + _duration;
    }

    public override void Movement()
    {
        float distance = Vector3.Distance(_myTransform.position, _destinationPos);
        Vector3 direction = (_destinationPos - _myTransform.position).normalized;


        if (distance > 0.2f)
        {
            _myTransform.position += direction * _speed * Time.deltaTime;
            _time = Time.time + _duration;
        }
        else
        {
            _myTransform.position = _destinationPos;
            StayTimer();
        }
    }

    public void StayTimer()
    {
        if (Time.time > _time)
        {
            Destroy(gameObject);
        }
    }
}
