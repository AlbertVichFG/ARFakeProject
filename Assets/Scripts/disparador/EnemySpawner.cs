using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 4.5f;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minSpawnInterval = 0.6f;

    [SerializeField] private float difficultyIncreaseTime = 10f;
    [SerializeField] private float spawnDecrease = 0.2f;

    private float spawnTimer;
    private float difficultyTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        // spawn enemics
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }

        // augment dificultat
        /*if (difficultyTimer >= difficultyIncreaseTime)
        {
            IncreaseDifficulty();
            difficultyTimer = 0;
        }*/
    }

    void SpawnEnemy()
    {
        // decidir costat spawn
        float side = Random.value < 0.5f ? -1f : 1f;

        // angle fora pantalla
        float horizontalAngle = Random.Range(70f, 110f) * side;

        // angle vertical (no peus ni sostre)
        float verticalAngle = Random.Range(-5f, 15f);

        Quaternion rot = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

        Vector3 dir = rot * player.forward;

        Vector3 spawnPos = player.position + dir * spawnDistance;

        GameObject prefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

   /* void IncreaseDifficulty()
    {
        // spawn ms rpd
        spawnInterval -= spawnDecrease;

        if (spawnInterval < minSpawnInterval)
            spawnInterval = minSpawnInterval;

        // enemics ms rpids
        EnemyController.enemySpeed += 0.2f;
    }*/

    void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.red;

        Vector3 center = player.position + player.forward * spawnDistance;

        Gizmos.DrawWireSphere(center, 1f);
    }
}
