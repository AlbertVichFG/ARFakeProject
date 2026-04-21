using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 8f;
    [SerializeField] private float spawnInterval = 2f;

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
    player.right * horizontal +
    player.up * vertical;

        GameObject prefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);


    }
}
