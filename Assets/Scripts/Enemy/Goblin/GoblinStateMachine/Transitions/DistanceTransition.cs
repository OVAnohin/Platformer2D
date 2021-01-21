using System.Linq;
using UnityEngine;

public class DistanceTransition : Transition
{
  [SerializeField] private float _transitionRange = 0;
  [SerializeField] private float _rangeSpread = 0.025f;

  private State _targetState = null;

  private void Awake()
  {
    _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
  }

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
    if (Vector2.Distance(transform.position, Target.transform.position) <= _transitionRange)
    {
      NeedTransit = true;
      _targetState = TargetStates.Where(value => value is GoblinAttackState).First();
    }
  }
}
