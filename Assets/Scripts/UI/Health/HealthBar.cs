using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private Player _player = default;
  [SerializeField] private Heart _prefabHeart = default;


  private List<Heart> _hearts = new List<Heart>();

  private void OnEnable()
  {
    _player.HealthChanged += OnHealthChanged;
  }


  private void OnDisable()
  {
    _player.HealthChanged -= OnHealthChanged;
  }

  private void OnHealthChanged(int currentHealt, int health)
  {
    if (currentHealt == health && _hearts.Count == 0)
    {
      for (int i = 0; i < health; i++)
      {
        Heart hearth = Instantiate(_prefabHeart, transform);
        _hearts.Add(hearth);
      }
    }

    if (health > _hearts.Count)
    {
      Heart hearth = Instantiate(_prefabHeart, transform);
      _hearts.Add(hearth);
    }

    if (currentHealt < health)
    {
      if (currentHealt < 0)
        currentHealt = 0;

      ChangeHeartsToLive(health);
      ChangeHeartsToBroken(currentHealt, health);
    }

    if (currentHealt == health)
      ChangeHeartsToLive(health);
  }

  private void ChangeHeartsToBroken(int currentHealt, int health)
  {
    for (int i = currentHealt; i < health; i++)
      _hearts[i].SetToBroken();
  }

  private void ChangeHeartsToLive(int health)
  {
    for (int i = 0; i < health; i++)
      _hearts[i].SetToLive();
  }
}