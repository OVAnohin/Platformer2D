using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GoblinStateMachine))]

public class Goblin : Enemy
{
  [SerializeField] private float _speed = 0;

  public override event UnityAction<Enemy> Dying;
  public Player GetTarget => Target;
  public float GetSpeed => _speed;

  public override void Init(Player player)
  {
    Target = player;
  }

  protected override void Die()
  {
    Target.AddMoney(Reward);
    Dying?.Invoke(this);
    GetComponent<GoblinStateMachine>().ResetOnDie();
    gameObject.SetActive(false);
  }
}
