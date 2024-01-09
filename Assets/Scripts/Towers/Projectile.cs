using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float _speed;
    protected Transform _myTransform;
    protected Transform _enemy;

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
    }

    public void SetEnemy(Transform enemy)
    {
        _enemy = enemy;
    }

    public abstract void Movement();

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.transform == _enemy)
            {
                Debug.Log(other.gameObject.name + " recieve damage");
                Destroy(gameObject);
            }
        }
    }
}
