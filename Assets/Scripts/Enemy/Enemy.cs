using System.Collections.Generic;
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

    protected LayerMask _structureToAttackLayer;

    [SerializeField] private Vector3 _deplacementOffset = Vector2.zero;
    [SerializeField] private List<Transform> _path;
    private int _pathIndex = 0;
    private Vector3 _target = Vector3.zero;

    private void Update()
    {
        Move();

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

    public void SetOffsetDeplacement(Vector2 offset)
    { _deplacementOffset = offset; }

    public void SetPath(List<Transform> path)
    { _path = path; }

    public void TakeDamage(int damage)
    { 
        _life -= damage; 

        if ( _life <= 0 )
            Death();
    }

    private void Death()
    { Destroy(gameObject); }

    protected abstract void Attack();

    protected abstract Collider[] CheckStructureToAttack();

    private void Move()
    {
        Collider[] structuresCollider = CheckStructureToAttack();

        if (structuresCollider == null)
        {
            if (Mathf.Abs(Vector3.Dot(transform.forward, structuresCollider[0].transform.position)) <= 0.05)
            {
                b_IsMoving = false;
                b_IsAttacking = true;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _path[_pathIndex].transform.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _path[_pathIndex].transform.position) < 0.01)
        {
            _pathIndex += 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.transform.TryGetComponent(out Gate gate))
        {
            gate.TakeDamage();
            Death();
        }
         */
    }
}
