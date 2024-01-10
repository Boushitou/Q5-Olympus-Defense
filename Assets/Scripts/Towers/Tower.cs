using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected int _health;
    protected int _damage;
    protected int _cost;
    protected float _atkSpeed;
    protected float _range;

    protected Transform _myTransform;

    private float _coolDown = 0f;
    private LayerMask _enemyMask;

    [SerializeField] public GameObject Projectile;
    [SerializeField] public Transform Origin;

    private void Awake()
    {
        _myTransform = transform;
        _enemyMask = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        Transform enemy = GetClosestEnemy();
        if (enemy != null)
        {
            if (Time.time >= _coolDown)
            {
                Attack(enemy);

                _coolDown = Time.time + _atkSpeed;
            }
        }
    }

    public Transform GetClosestEnemy()
    {
        Collider[] hitCollider = Physics.OverlapSphere(_myTransform.position, _range, _enemyMask);
        Transform closestEnemy = null;
        float closestDist = Mathf.Infinity;

        foreach (Collider collider in hitCollider)
        {
            float dist = Vector3.Distance(collider.transform.position, -_myTransform.position);

            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnemy = collider.transform;
            }
        }

        if (closestEnemy != null)
        {
            Debug.Log(closestEnemy.name);
        }

        return closestEnemy;
    }

    public abstract void InitializeValue();

    public abstract void Attack(Transform enemy);

    public int GetCost() { return _cost; }
}