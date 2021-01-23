using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected int Reward;

    public abstract event UnityAction<Enemy> Dying;

    protected Player Target;

    public abstract void Init(Player player);

    public int GetReward => Reward;

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
