using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class GoblinAttackState : State
{
  [SerializeField] private int _damage = 1;
  [SerializeField] private float _delay = 1f;

  private Animator _animator;

  public float Dalay
  {
    get { return _delay; }
  }

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  private void OnEnable()
  {
    Target.TakeDamage(_damage);
    _animator.Play("Attack");
  }
}
