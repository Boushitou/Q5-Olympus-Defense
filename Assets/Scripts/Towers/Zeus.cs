using UnityEngine;

public class Zeus : Tower
{
    public override void Attack(Transform enemy)
    {
        if (Projectile != null)
        {
            Vector3 direction = (enemy.position - _myTransform.position).normalized;
            GameObject cloud = Instantiate(Projectile, Origin.position, Quaternion.LookRotation(direction));

            cloud.GetComponent<Projectile>().SetValues(enemy, _damage);
        }
    }

    public override void InitializeValue()
    {
        _maxHealth = 200;
        _damage = 20;
        _atkSpeed = 3f;
        _range = 12f;
        _cost = 400;
    }
}
