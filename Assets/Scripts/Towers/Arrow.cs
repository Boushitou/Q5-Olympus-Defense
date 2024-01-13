using UnityEngine;

public class Arrow : Projectile
{
    private void Start()
    {
        _speed = 50f;
    }

    public override void Movement()
    {
        Vector3 direction = (_enemy.GetComponent<Collider>().bounds.center - _myTransform.position).normalized;

        _myTransform.position += direction * _speed * Time.deltaTime;
        _myTransform.rotation = Quaternion.LookRotation(direction);
    }
}
