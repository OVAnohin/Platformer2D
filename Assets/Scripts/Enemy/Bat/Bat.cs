using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bat : Enemy
{
    public override event UnityAction<Enemy> Dying;
    public Player GetTarget => Target;

    public override void Init(Player player)
    {
        Target = player;
    }

    protected override void Die()
    {
        Target.AddMoney(Reward);
        Dying?.Invoke(this);
        gameObject.SetActive(false);
    }
}
