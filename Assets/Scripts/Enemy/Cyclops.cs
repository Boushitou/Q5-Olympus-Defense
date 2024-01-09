using System.Collections;
using UnityEngine;

public class Cyclops : Enemy
{
    [SerializeField] private float _radiusCheckTower = 10;

    private void Awake()
    {
        _life = 200;
        _speed = 3;
        _damage = 10;
        _timeWaitAttack = 1;
        _belief = 40;

        _structureToAttackLayer = LayerMask.GetMask("Tower");
    }

    private Collider[] /*Tower[]*/ GetTowers()
    {
        Collider[] towersCollider = Physics.OverlapSphere(transform.position, _radiusCheckTower, _structureToAttackLayer);

        /*Tower[] towers = new Tower[towersCollider.Length];
        
        for (int i = 0; i < towersCollider.Length; i++)
        {
            towers[i] = towersCollider[i].GetComponent<Tower>();
        }

        return towers;
        */

        return Physics.OverlapSphere(transform.position, _radiusCheckTower, _structureToAttackLayer);
    }

    protected override void Attack()
    {
        if (!b_IsAttacking)
        {
            Collider[] towers = GetTowers();

            if (towers.Length > 0)
            {
                b_IsAttacking = true;
                StartCoroutine(AttackTower(towers[0].gameObject));
            }
        }
    }

    private IEnumerator AttackTower(GameObject tower)
    {
        while (tower.activeSelf)
        {
            //tower.TakeDamage(_damage);
            Debug.Log("tower take damage");
            Debug.Log(Vector3.Distance(tower.transform.position, transform.position));
            yield return new WaitForSeconds(_timeWaitAttack);
        }

        b_IsAttacking = false;
        b_HasToAttack = false;
    }

    protected override Collider[] CheckStructureToAttack()
    {
        return Physics.OverlapSphere(transform.position, _radiusCheckTower, _structureToAttackLayer);
    }
}
