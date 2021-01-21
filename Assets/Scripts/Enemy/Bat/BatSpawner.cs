using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : BatObjectPool
{
  [SerializeField] private Bat _enemyPrefab = default;
  [SerializeField] private float _secondBetweenSpawn = default;
  [SerializeField] private Transform[] _spawnPoints = default;
  [SerializeField] private Player _target = default;

  private float _elapsedTime = 0;

  private void Start()
  {
    Init(_enemyPrefab);
  }

  private void Update()
  {
    if (_elapsedTime <= 0)
    {
      if (TryGetObject(out Bat bat))
      {
        _elapsedTime = _secondBetweenSpawn;
        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
        SetBatActivTrue(bat, _spawnPoints[spawnPointNumber].position);
      }
    }

    _elapsedTime -= Time.deltaTime;
  }

  private void SetBatActivTrue(Bat bat, Vector2 spawnPoint)
  {
    bat.Init(_target);
    bat.gameObject.SetActive(true);
    bat.gameObject.transform.position = spawnPoint;
  }
}
