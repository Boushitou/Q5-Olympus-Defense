using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected int _life = 1;
    protected float _speed = 1;
    protected int _damage = 1;
    protected float _timeWaitAttack = 1;
    protected int _belief = 1;

    protected bool b_IsMoving = false;
    protected bool b_IsAttacking = false;

    private void Update()
    {
        //Move();
        Attack();

        /*if (!b_IsMoving)
        {
            Attack();
        }
        else if (!b_IsAttacking)
        {
            Move();
        }*/
    }

    public int GetLife()
    { return _life; }

    public float GetSpeed() 
    { return _speed;}

    public float GetDamage() 
    { return _damage;}

    public int GetBelief()
    { return _belief; }

    public void SetSpeed(float speed)
    { _speed = speed; }

    public void TakeDamage(int damage)
    { 
        _life -= damage; 

        if ( _life <= 0 )
            Death();
    }

    private void Death()
    { Destroy(gameObject); }

    public abstract void Attack();

    private void Move()
    {

    }
}
