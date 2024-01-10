using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Cyclops : Enemy
{
    private void Awake()
    {
        _life = 200;
        _speed = 3;
        _damage = 10;
        _timeWaitAttack = 1;
        _belief = 40;

        _structureToAttackLayer = LayerMask.GetMask("Tower");
    }

    private Collider[] /*Tower[]*/ GetTowersAround()
    {
        /*Collider[] towersCollider = Physics.OverlapSphere(transform.position, _radiusCheckStructure, _structureToAttackLayer);

        Tower[] towers = new Tower[towersCollider.Length];
        
        for (int i = 0; i < towersCollider.Length; i++)
        {
            towers[i] = towersCollider[i].GetComponent<Tower>();
        }

        return towers;
        */

        return Physics.OverlapSphere(transform.position, _radiusCheckStructure, _structureToAttackLayer);
    }

    protected override void Attack()
    {
        if (!b_IsAttacking)
        {
            Collider[] towers = GetTowersAround();

            if (towers.Length > 0)
            {
                _transform.LookAt(towers[0].transform.position);
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
    { return Physics.OverlapSphere(transform.position, _radiusCheckStructure, _structureToAttackLayer); }
}
