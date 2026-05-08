using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 6f;

    [SerializeField] private float spawnInterval = 2f;

    // límits altura spawn
    [SerializeField] private float minHeight = -1f;
    [SerializeField] private float maxHeight = 2f;

    // límits laterals
    [SerializeField] private float horizontalRange = 4f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();

            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        // posició davant jugador
        Vector3 center =
            player.position +
            player.forward * spawnDistance;

        // offset random
        float randomX =
            Random.Range(-horizontalRange, horizontalRange);

        float randomY =
            Random.Range(minHeight, maxHeight);

        Vector3 spawnPos =
            center +
            player.right * randomX +
            player.up * randomY;

        // prefab random
        GameObject prefab =
            enemyPrefabs[
                Random.Range(0, enemyPrefabs.Length)
            ];

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    // veure zona spawn al editor
    void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.red;

        Vector3 center =
            player.position +
            player.forward * spawnDistance;

        Gizmos.DrawWireCube(
            center,
            new Vector3(
                horizontalRange * 2,
                maxHeight - minHeight,
                1f
            )
        );
    }
}
