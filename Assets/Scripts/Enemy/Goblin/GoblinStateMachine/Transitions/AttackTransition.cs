using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GoblinAttackState))]
public class AttackTransition : Transition
{
    private State _targetState;
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
            _targetState = TargetStates.Find(value => value is GoblinSearchTargetState);
        }

        _elapsedTime += Time.deltaTime;
    }
}
