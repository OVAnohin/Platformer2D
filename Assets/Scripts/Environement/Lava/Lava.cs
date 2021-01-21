using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.TryGetComponent<Player>(out Player player))
      player.TakeDamage(1000);

    if (collision.TryGetComponent<Enemy>(out Enemy enemy))
      enemy.TakeDamage(1000);

  }
}
