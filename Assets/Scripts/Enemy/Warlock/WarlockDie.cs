using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockDie : MonoBehaviour
{
  [SerializeField] private Warlock _warlock = default;
  [SerializeField] private GameObject _deathEffect = default;

  private void OnEnable()
  {
    _warlock.Dying += OnWarlockDied;
  }

  private void OnDisable()
  {
    _warlock.Dying -= OnWarlockDied;
  }

  private void OnWarlockDied(Enemy warlock)
  {
    Instantiate(_deathEffect, warlock.transform.position, Quaternion.identity);
  }
}
