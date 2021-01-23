using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GoblinAttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Target.TakeDamage(_damage);
        _animator.Play("Attack");
    }

    public float Dalay
    {
        get { return _delay; }
    }
}
