using System.Collections;
using UnityEngine;

public class Cyclops : Enemy
{
    private void Awake()
    {
        _maxLife = 200;
        _life = _maxLife;
        _speed = 3;
        _damage = 10;
        _timeWaitAttack = 1;
        _belief = 40;

        _structureToAttackLayer = LayerMask.GetMask("Tower");
    }

    private Tower[] GetTowersAround()
    {
        Collider[] towersCollider = Physics.OverlapSphere(transform.position, _radiusCheckStructure, _structureToAttackLayer);

        Tower[] towers = new Tower[towersCollider.Length];

        for (int i = 0; i < towersCollider.Length; i++)
        {
            towers[i] = towersCollider[i].GetComponent<Tower>();
        }

        return towers;
    }

    protected override void Attack()
    {
        if (!b_IsAttacking)
        {
            Tower[] towers = GetTowersAround();

            if (towers.Length > 0)
            {
                _transform.LookAt(towers[0].transform.position);
                b_IsAttacking = true;
                StartCoroutine(AttackTower(towers[0]));
            }
        }
    }

    private IEnumerator AttackTower(Tower tower)
    {
        while (tower.GetHealth() > 0)
        {
            tower.TakeDamage(_damage);
            yield return new WaitForSeconds(_timeWaitAttack);
        }

        b_IsAttacking = false;
        b_HasToAttack = false;
    }

    protected override Collider[] CheckStructureToAttack()
    { return Physics.OverlapSphere(transform.position, _radiusCheckStructure, _structureToAttackLayer); }
}
