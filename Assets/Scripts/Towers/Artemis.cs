using UnityEngine;

public class Artemis : Tower
{
    private void Start()
    {
        _health = 100;
        _damage = 30;
        _cost = 100;
        _atkSpeed = 0.3f;
        _range = 15f;
    }

    public override void Attack(Transform enemy)
    {
        if (Projectile != null)
        {
            Vector3 direction = (enemy.position - _myTransform.position).normalized;
            GameObject arrow = Instantiate(Projectile, Origin.position, Quaternion.LookRotation(direction));

            arrow.GetComponent<Projectile>().SetEnemy(enemy);
        }
    }
}
