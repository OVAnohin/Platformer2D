using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected Sprite Sprite;
    [SerializeField] protected string Label;

    public float GetAttackRange => AttackRange;
    public Sprite GetSprite => Sprite;
    public int GetDamage => Damage;
    public string GetLabel => Label;
    public bool IsUse { get; set; }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.Equip(this);
            gameObject.SetActive(false);
        }
    }
}
