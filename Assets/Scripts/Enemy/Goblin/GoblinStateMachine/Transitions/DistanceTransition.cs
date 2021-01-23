using System.Linq;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

    public override State TargetState
    {
        get { return _targetState; }
    }

    private State _targetState;

    private void Awake()
    {
        _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) <= _transitionRange)
        {
            NeedTransit = true;
            _targetState = TargetStates.Where(value => value is GoblinAttackState).First();
        }
    }
}
