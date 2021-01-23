using UnityEngine;

public class GoblinSpawner : GoblinObjectPool
{
    [SerializeField] private Goblin _enemyPrefab;
    [SerializeField] private float _secondBetweenSpawn;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _target;

    private Camera _camera;
    private float _elapsedTime = 0;

    private void Start()
    {
        _camera = Camera.main;
        Init(_enemyPrefab);
    }

    private void Update()
    {
        Vector3 point = _camera.WorldToViewportPoint(_spawnPoint.position);

        if (point.x > 0.01f && point.x < 0.93f)
        {
            if (_elapsedTime <= 0)
            {
                _elapsedTime = _secondBetweenSpawn;
                if (TryGetObject(out Goblin goblin))
                    SetBatActivTrue(goblin, _spawnPoint.position);
            }

            _elapsedTime -= Time.deltaTime;
        }
    }

    private void SetBatActivTrue(Goblin goblin, Vector2 spawnPoint)
    {
        goblin.Init(_target);
        goblin.gameObject.SetActive(true);
        goblin.gameObject.transform.position = spawnPoint;
    }
}
