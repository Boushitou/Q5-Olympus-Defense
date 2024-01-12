using UnityEngine;

public class Thunder : MonoBehaviour
{
    private int _damage = 10;
    private float _range = 10f;
    private float _cooldown = 0;
    private float _atkSpeed = 0.5f;
    private bool b_canAttack = true;

    private LayerMask _enemyMask;

    private void Awake()
    {
        _enemyMask = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        if (!b_canAttack)
        {
            if (Time.time > _cooldown)
            {
                _cooldown = Time.time + _atkSpeed;
                b_canAttack = true;
            }
        }
        else
        {
            AttackInRange();
        }
    }

    public void AttackInRange()
    {
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, _range, _enemyMask);

        foreach (Collider collider in hitCollider)
        {
            collider.GetComponent<Enemy>().TakeDamage(_damage);
        }

        b_canAttack = false;
    }
}
