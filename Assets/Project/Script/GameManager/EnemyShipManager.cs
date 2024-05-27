using UnityEngine;

public class EnemyShipManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyShipPrefab;
    [SerializeField] float _spawnDelay = 5f;
    [SerializeField] float _minSpawnRange = 500f, _maxSpawnRange = 2000f;
    [SerializeField] LayerMask _collisionMask; // Thêm LayerMask để kiểm tra va chạm

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
        Vector3 spawnPosition;
        bool validSpawnPosition = false;
        int attempts = 0;

        do
        {
            spawnPosition = Random.insideUnitSphere * Random.Range(_minSpawnRange, _maxSpawnRange);
            spawnPosition.y = 0; // Giữ kẻ thù trên mặt phẳng nếu cần thiết

            // Kiểm tra xem vị trí spawn có hợp lệ hay không
            if (!Physics.CheckSphere(spawnPosition, 1f, _collisionMask))
            {
                validSpawnPosition = true;
            }

            attempts++;
        } while (!validSpawnPosition && attempts < 30); // Giới hạn số lần thử

        if (validSpawnPosition)
        {
            Instantiate(_enemyShipPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Failed to find a valid spawn position for enemy ship.");
        }
    }
}
