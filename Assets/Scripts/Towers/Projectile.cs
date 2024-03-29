using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float _speed;
    protected Transform _myTransform;
    protected Transform _enemy;
    protected int _damage;

    private void Awake()
    {
        _myTransform = transform;
    }

    private void Update()
    {
        if (_enemy != null)
        {
            Movement();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetValues(Transform enemy, int damage)
    {
        _enemy = enemy;
        _damage = damage;
    }

    public abstract void Movement();

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.transform == _enemy)
            {
                _enemy.GetComponent<Enemy>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
