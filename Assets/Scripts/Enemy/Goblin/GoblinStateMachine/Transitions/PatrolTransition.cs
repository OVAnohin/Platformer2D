using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GoblinSearchTargetState))]

public class PatrolTransition : Transition
{
  [SerializeField] private float _timePatrol = 1f;

  private float _elapsedTime = 0;

  public override State TargetState
  {
    get { return TargetStates[0]; }
  }

  private void OnEnable()
  {
    NeedTransit = false;
    _elapsedTime = 0;
    if (GetComponent<GoblinSearchTargetState>().IsTargetFound)
      enabled = false;
  }

  private void Update()
  {
    if (_elapsedTime > _timePatrol)
      NeedTransit = true;

    _elapsedTime += Time.deltaTime;
  }
}
