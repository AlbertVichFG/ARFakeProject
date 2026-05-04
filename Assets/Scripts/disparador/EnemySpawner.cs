using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 4f;
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    void Start()
    {
        if (player == null)
            player = Camera.main.transform;
    }

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
        float side = Random.value < 0.5f ? -1f : 1f;

        float horizontalAngle = Random.Range(60f, 90f) * side;
        float verticalAngle = Random.Range(-5f, 15f);

        Quaternion rot = Quaternion.Euler(verticalAngle, horizontalAngle, 0);
        Vector3 dir = rot * player.forward;

        Vector3 spawnPos = player.position + dir * spawnDistance;

        GameObject prefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
