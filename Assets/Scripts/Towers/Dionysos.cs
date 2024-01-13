using UnityEngine;

public class Dionysos : Tower
{
    public override void Attack(Transform enemy)
    {
        if (Projectile != null)
        {
            Vector3 direction = (enemy.position - _myTransform.position).normalized;
            GameObject bottle = Instantiate(Projectile, Origin.position, Quaternion.LookRotation(direction));

            bottle.GetComponent<Projectile>().SetValues(enemy, _damage);
        }
    }

    public override void InitializeValue()
    {
        _health = 80;
        _damage = 10;
        _cost = 200;
        _atkSpeed = 1f;
        _range = 13f;
    }
}
