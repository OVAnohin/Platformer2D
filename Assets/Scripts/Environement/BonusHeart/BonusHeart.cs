using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHeart : Health
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.TakeBonusHealth();
            AudioSource.clip = TakeSound;
            AudioSource.Play();
            Destroy(gameObject, 0.3f);
        }
    }
}
