using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
  [SerializeField] protected List<State> TargetStates;

  public bool NeedTransit { get; protected set; }
  protected Player Target { get; private set; }

  public abstract State TargetState { get; }

  public void Init(Player target)
  {
    Target = target;
  }

  private void OnEnabled()
  {
    NeedTransit = false;
  }
}
