using System.Linq;
using UnityEngine;

public class GapTransition : Transition
{
    [SerializeField] private Transform _gapCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _distanceCheck;

    public override State TargetState
    {
        get { return _targetState; }
    }

    private State _targetState;

    private void OnEnable()
    {
        NeedTransit = false;
    }

    private void Update()
    {
        bool isGap = Physics2D.Raycast(_gapCheck.position, Vector2.down, _distanceCheck, _whatIsGround);
        if (!isGap)
        {
            NeedTransit = true;
            _targetState = TargetStates.Where(value => value is GoblinJumpState).First();
        }
    }
}
