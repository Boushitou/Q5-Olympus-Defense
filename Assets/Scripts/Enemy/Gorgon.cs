using System.Collections;
using UnityEngine;

public class Gorgon : Enemy
{
    private void Awake()
    {
        _maxLife = 100;
        _life = _maxLife;
        _speed = 5;
        _damage = 5;
        _timeWaitAttack = 0.5f;
        _belief = 15;

        _structureToAttackLayer = LayerMask.GetMask("Altar");
    }

    private Altar[] GetAltarsAround()
    {
        Collider[] altarsCollider = Physics.OverlapSphere(transform.position, _radiusCheckStructure, _structureToAttackLayer);

        Altar[] altars = new Altar[altarsCollider.Length];

        for (int i = 0; i < altarsCollider.Length; i++)
        {
            altars[i] = altarsCollider[i].GetComponent<Altar>();
        }

        return altars;
    }

    protected override void Attack() 
    { 
        if (!b_IsAttacking)
        {
            Altar[] altars = GetAltarsAround();

            if (altars.Length > 0)
            {
                _transform.LookAt(altars[0].transform.position);
                b_IsAttacking = true;
                StartCoroutine(AttackAltar(altars[0]));
            }
        }
    }

    private IEnumerator AttackAltar(Altar altar)
    {
        while (altar.GetHealth() > 0)
        {
            altar.TakeDamage(_damage);
            yield return new WaitForSeconds(_timeWaitAttack);
        }

        b_IsAttacking = false;
        b_HasToAttack = false;
    }

    protected override Collider[] CheckStructureToAttack()
    { return Physics.OverlapSphere(transform.position, _radiusCheckStructure, _structureToAttackLayer); }
}
