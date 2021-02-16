using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;
    [SerializeField] private GameObject _deathEffect;

    private List<Goblin> _pool = new List<Goblin>();
    private Goblin _prefab;

    protected void Init(Goblin prefab)
    {
        _prefab = prefab;

        for (int i = 0; i < _capacity; i++)
            SpawnGoblin();
    }

    private void SpawnGoblin()
    {
        Goblin spawned = Instantiate(_prefab, _container.transform);
        spawned.Dying += OnSpawnedDyed;
        spawned.gameObject.SetActive(false);
        _pool.Add(spawned);
    }

    private void OnDisable()
    {
        foreach (var item in _pool)
            item.Dying -= OnSpawnedDyed;
    }

    private void OnSpawnedDyed(Enemy enemy)
    {
        Instantiate(_deathEffect, enemy.transform.position, Quaternion.identity);
    }

    protected Goblin GetGoblin()
    {
        if (TryGetFreeGoblin() == null)
            SpawnGoblin();

        return TryGetFreeGoblin();
    }

    private Goblin TryGetFreeGoblin()
    {
        return _pool.Find(p => p.gameObject.activeSelf == false);
    }
}
