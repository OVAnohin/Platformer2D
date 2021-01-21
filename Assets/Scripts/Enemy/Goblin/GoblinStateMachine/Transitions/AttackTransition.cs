using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GoblinAttackState))]

public class AttackTransition : Transition
{
  private State _targetState = null;

  private float _elapsedTime = 0;

  public override State TargetState
  {
    get { return _targetState; }
  }

  private void OnEnable()
  {
    NeedTransit = false;
    _elapsedTime = 0;
  }

  private void Update()
  {
    if (_elapsedTime > GetComponent<GoblinAttackState>().Dalay)
    {
      NeedTransit = true;
      _targetState = TargetStates.Where(value => value is GoblinSearchTargetState).First();
    }

    _elapsedTime += Time.deltaTime;
  }
}
