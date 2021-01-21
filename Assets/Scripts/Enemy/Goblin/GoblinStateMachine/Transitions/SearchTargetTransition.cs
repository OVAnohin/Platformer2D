using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GoblinSearchTargetState))]

public class SearchTargetTransition : Transition
{
  private State _targetState = null;
  public override State TargetState
  {
    get { return _targetState; }
  }

  private void OnEnable()
  {
    NeedTransit = false;
  }

  private void Update()
  {
    if (GetComponent<GoblinSearchTargetState>().IsEndSearch)
    {
      NeedTransit = true;
      _targetState = (from value in TargetStates
                      where value is GoblinMoverState
                      select value).First();
    }

  }
}
