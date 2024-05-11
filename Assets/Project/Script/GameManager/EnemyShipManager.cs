using UnityEngine;

public class EnemyShipManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyShipPrefab;
    [SerializeField] float _spawnDelay = 10f;
    [SerializeField] float _minSpawnRange = 500f, _maxSpawnRange = 2000f;

    float _spawnTimer;

    void Awake()
    {
        _spawnTimer = _spawnDelay;
    }

    void Update()
    {
        if (_spawnTimer <= 0f)
        {
            SpawnEnemyShip();
            enabled = false; // Tắt component này sau khi sinh ra kẻ thù
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        }
    }

    void SpawnEnemyShip()
    {
        var spawnPosition = Random.insideUnitSphere * Random.Range(_minSpawnRange, _maxSpawnRange);
        Instantiate(_enemyShipPrefab, spawnPosition, Quaternion.identity);
    }
}
