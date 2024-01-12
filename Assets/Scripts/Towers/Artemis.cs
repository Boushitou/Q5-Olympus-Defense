using UnityEngine;

public class Artemis : Tower
{
    public override void Attack(Transform enemy)
    {
        if (Projectile != null)
        {
            Vector3 direction = (enemy.position - _myTransform.position).normalized;
            GameObject arrow = Instantiate(Projectile, Origin.position, Quaternion.LookRotation(direction));

            arrow.GetComponent<Projectile>().SetValues(enemy, _damage);
        }
    }

    public override void InitializeValue()
    {
        _health = 50;
        _damage = 15;
        _cost = 100;
        _atkSpeed = 0.5f;
        _range = 20f;
    }
}
