using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Warlock : Enemy
{
  [SerializeField] private Player _player = default;

  public override event UnityAction<Enemy> Dying;

  public override void Init(Player target)
  {
    Target = target;
  }
  public Player GetTarget => Target;

  private void Awake()
  {
    Init(_player);
  }

  protected override void Die()
  {
    Target.AddMoney(Reward);
    Dying?.Invoke(this);
    Destroy(gameObject);
  }
}
