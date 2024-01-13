using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float _speed;
    protected Transform _myTransform;
    protected Transform _enemy;
    protected int _damage;

    private LayerMask _groundLayer;
    protected Vector3 _direction = Vector3.zero;

    private void Awake()
    {
        _myTransform = transform;

        _groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        Movement();

        /*if (_enemy != null)
        {
            Movement();
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    public void SetValues(Transform enemy, int damage)
    {
        _enemy = enemy;
        _damage = damage;
    }

    public abstract void Movement();

    public virtual void Effect()
    {
        _enemy.GetComponent<Enemy>().TakeDamage(_damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.transform == _enemy)
            {
                Effect();
                Destroy(gameObject);
            }
            else if (other.gameObject.layer == _groundLayer)
            {
                Destroy(gameObject);
            }
        }
    }
}
