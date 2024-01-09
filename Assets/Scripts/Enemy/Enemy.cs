using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected int _life = 1;
    protected float _speed = 1;
    protected int _damage = 1;
    protected float _timeWaitAttack = 1;
    protected int _belief = 1;

    protected bool b_IsAttacking = false;
    protected bool b_HasToAttack = false;

    protected LayerMask _structureToAttackLayer;

    private Transform _transform;
    private Vector3 _deplacementOffset = Vector2.zero;
    [SerializeField] private List<Transform> _path;
    private int _pathIndex = 0;
    private Vector3 _target = Vector3.zero;

    private void Start()
    {
        _transform = transform;

        _target = _path[0].transform.position + _deplacementOffset;
    }

    private void Update()
    {
        if (b_HasToAttack)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    public int GetLife()
    { return _life; }

    public float GetSpeed() 
    { return _speed;}

    public float GetDamage() 
    { return _damage;}

    public int GetBelief()
    { return _belief; }

    public void SetValues(Vector2 offsetDeplacement, List<Transform> path)
    {
        _deplacementOffset = offsetDeplacement;
        _path = path;
    }

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

    private void UpdateTarget()
    {
        if (_path[_pathIndex].transform.position.x < _path[_pathIndex + 1].transform.position.x)
        {

        }
        else if (_path[_pathIndex].transform.position.x > _path[_pathIndex + 1].transform.position.x)
        {

        }
        else if (_path[_pathIndex].transform.position.y < _path[_pathIndex + 1].transform.position.y)
        {
            _target = _path[_pathIndex].transform.position + _deplacementOffset;
        }
        else if (_path[_pathIndex].transform.position.y > _path[_pathIndex + 1].transform.position.y)
        {
            _target = _path[_pathIndex].transform.position + _deplacementOffset;
        }

        if (_deplacementOffset.z < 0)
        {
            
        }
        else if (_deplacementOffset.z > 0)
        {
            
        }

        _pathIndex += 1;
    }

    private void Move()
    {
        Collider[] structuresCollider = CheckStructureToAttack();

        if (structuresCollider.Length > 0)
        {
            float dot = Vector3.Dot(_transform.forward, structuresCollider[0].transform.position - _transform.position);
            float abs = Mathf.Abs(dot);
            b_HasToAttack = abs <= 0.05;
        }

        _transform.position = Vector3.MoveTowards(_transform.position, _path[_pathIndex].transform.position, _speed * Time.deltaTime);
        _transform.LookAt(_path[_pathIndex].transform.position);
        //target.position

        if (Vector3.Distance(_transform.position, _path[_pathIndex].transform.position) < 0.01)
        {
            if (_pathIndex == _path.Count - 1)
            {
                //Destroy(gameObject);
                if (!b_IsAttacking)
                {
                    b_IsAttacking = true;
                    StartCoroutine(AttackGate());
                }
            }
            else
            {
                _pathIndex += 1;
            }
        }
    }

    private IEnumerator AttackGate()
    {
        while (_path[_pathIndex].gameObject.activeSelf /*Gate.Instance.GetLife() > 0*/)
        {
            Debug.Log("gate take damage");
            yield return new WaitForSeconds(_timeWaitAttack);
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
