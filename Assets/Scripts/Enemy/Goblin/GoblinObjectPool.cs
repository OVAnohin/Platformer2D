using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoblinObjectPool : MonoBehaviour
{
  [SerializeField] private GameObject _container = default;
  [SerializeField] private int _capacity = default;
  [SerializeField] private GameObject _deathEffect = default;

  private List<Goblin> _pool = new List<Goblin>();

  protected void Init(Goblin prefab)
  {
    for (int i = 0; i < _capacity; i++)
    {
      Goblin spawned = Instantiate(prefab, _container.transform);
      spawned.Dying += OnSpawnedDyed;
      spawned.gameObject.SetActive(false);
      _pool.Add(spawned);
    }
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

  protected bool TryGetObject(out Goblin result)
  {
    result = _pool.Where(p => p.gameObject.activeSelf == false).FirstOrDefault();

    return result != null;
  }

  public void ResetPool()
  {
    foreach (var item in _pool)
      item.gameObject.SetActive(false);
  }
}
