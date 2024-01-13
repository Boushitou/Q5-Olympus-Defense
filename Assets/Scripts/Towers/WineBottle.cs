using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WineBottle : Projectile
{
    private float _range = 15f;
    private LayerMask _enemyMask;

    private void Start()
    {
        _enemyMask = LayerMask.GetMask("Enemy");
        _speed = 30f;
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

    public override void Effect()
    {
        Vector3 enemyPos = _enemy.transform.position;

        Collider[] hitCollider = Physics.OverlapSphere(enemyPos, _range, _enemyMask);

        foreach (Collider collider in hitCollider)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();

            enemy.Slowed(2f);
            _enemy.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }
}
