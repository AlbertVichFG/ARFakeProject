using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 8f;
    [SerializeField] private float spawnInterval = 2f;

    [SerializeField] private float spawnWidth = 2f;
    [SerializeField] private float spawnHeight = 1f;

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
        float horizontal = Random.Range(-4f, 4f);
        float vertical = Random.Range(-1f, 2f);

        Vector3 spawnPos =
            player.position +
            player.forward * spawnDistance +
            player.right * Random.Range(-2f, 2f) +
            player.up * Random.Range(-0.3f, 0.8f);

        GameObject prefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);


    }

    void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.red;

        Vector3 center = player.position + player.forward * spawnDistance;

        Gizmos.DrawWireCube(
            center,
            new Vector3(spawnWidth * 2, spawnHeight * 2, 0.1f)
        );
    }
}
