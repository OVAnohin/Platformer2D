using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
  [SerializeField] private Weapon[] _weaponsPrefab = default;
  [SerializeField] private Transform[] _spawnPoints = default;


  private void Start()
  {
    foreach (Transform elem in _spawnPoints)
      Instantiate(_weaponsPrefab[Random.Range(0, _weaponsPrefab.Length)], elem);
  }
}
