using UnityEngine;

[RequireComponent(typeof(GoblinSearchTargetState))]
public class SearchTargetTransition : Transition
{
    public override State TargetState
    {
        get { return _targetState; }
    }

    private State _targetState = null;

    private void OnEnable()
    {
        NeedTransit = false;
    }

    private void Update()
    {
        if (GetComponent<GoblinSearchTargetState>().IsEndSearch)
        {
            NeedTransit = true;

            _targetState = TargetStates.Find(value => value is GoblinMoverState);
        }
    }
}
