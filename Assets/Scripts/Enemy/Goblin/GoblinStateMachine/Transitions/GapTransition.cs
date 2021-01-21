using System.Linq;
using UnityEngine;

public class GapTransition : Transition
{
  [SerializeField] private Transform _gapCheck = null;
  [SerializeField] private LayerMask _whatIsGround = 0;
  [SerializeField] private float _distanceCheck = 1f;

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
    bool isGap = Physics2D.Raycast(_gapCheck.position, Vector2.down, _distanceCheck, _whatIsGround);
    if (!isGap)
    {
      NeedTransit = true;
      _targetState = TargetStates.Where(value => value is GoblinJumpState).First();
    }
  }
}
