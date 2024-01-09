using UnityEngine;

public class Harpy : Enemy
{
    private void Awake()
    {
        _life = 150;
        _speed = 7;
        _damage = 7;
        _timeWaitAttack = 0.75f;
        _belief = 20;

        _structureToAttackLayer = LayerMask.GetMask("Gate");
    }

    protected override void Attack(){}

    protected override Collider[] CheckStructureToAttack()
    { return new Collider[0]; }
}
