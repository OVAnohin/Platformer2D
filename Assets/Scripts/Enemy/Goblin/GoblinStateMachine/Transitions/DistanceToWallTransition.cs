using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DistanceToWallTransition : Transition
{
  [SerializeField] private LayerMask _whatIsGround = 0;
  [SerializeField] private float _wallCheckDistance = 0;

  public override State TargetState
  {
    get { return TargetStates[0]; }
  }

  private void OnEnable()
  {
    NeedTransit = false;
  }

  private void Update()
  {
    Vector2 _directionMove;
    if (transform.rotation.y != 0)
      _directionMove = Vector2.right;
    else
      _directionMove = Vector2.left;

    if (CastRayToWall(_directionMove))
    {
      NeedTransit = true;
    }
  }

  private bool CastRayToWall(Vector2 direction)
  {
    return Physics2D.Raycast(transform.position, direction, _wallCheckDistance, _whatIsGround);
  }
}
