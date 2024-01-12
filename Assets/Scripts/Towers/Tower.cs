using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected int _maxHealth;
    protected int _health;
    protected int _damage;
    protected int _cost;
    protected float _atkSpeed;
    protected float _range;

    protected Transform _myTransform;

    private float _coolDown = 0f;
    private LayerMask _enemyMask;
    private GameObject _tile;

    [SerializeField] public GameObject Projectile;
    [SerializeField] public Transform Origin;
    [SerializeField] private LifeBar _lifeBar;

    private void Awake()
    {
        _myTransform = transform;
        _enemyMask = LayerMask.GetMask("Enemy");
    }

    private void Start()
    {
        InitializeValue();
        _health = _maxHealth;
    }

    private void Update()
    {
        Transform enemy = GetClosestEnemy();
        if (enemy != null)
        {
            Debug.Log(enemy.name);
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

    public void TakeDamage(int damage)
    {
        _health -= damage;

        _lifeBar.UpdateLife(_maxHealth, _health);

        if (_health <= 0)
        {
            _tile.tag = "Constructible";
            Destroy(gameObject);
        }
    }

    public abstract void InitializeValue();

    public void SetTile(GameObject tile)
    {
        _tile = tile;
    }

    public abstract void Attack(Transform enemy);

    public int GetCost() { return _cost; }
    public int GetHealth() { return _health; }
}