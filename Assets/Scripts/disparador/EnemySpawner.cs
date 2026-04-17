using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistance;
    [SerializeField] private float spawnInterval;

    [SerializeField] private float minVerticalAngle;
    [SerializeField] private float maxVerticalAngle;

    [SerializeField] private float minHorizontalAngle;
    [SerializeField] private float maxHorizontalAngle;

    [Header("Enemy Movement")]
    [SerializeField] private float enemySpeed;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }

        MoveEnemies();
    }

    private void SpawnEnemy()
    {
        float side = Random.value < 0.5f ? -1 : 1;

        float hAngle = Random.Range(minHorizontalAngle, maxHorizontalAngle) * side;
        float vAngle = Random.Range(minVerticalAngle, maxVerticalAngle);

        Quaternion rot = Quaternion.Euler(vAngle, hAngle, 0);

        Vector3 dir = rot * Camera.main.transform.forward;

        Vector3 spawnPos = Camera.main.transform.position + dir * spawnDistance;

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);

        enemy.tag = "Enemy";
    }

    private void MoveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies)
        {
            Vector3 playerPos = Camera.main.transform.position;

            Vector3 dir = (playerPos - e.transform.position).normalized;

            e.transform.position += dir * enemySpeed * Time.deltaTime;

            e.transform.LookAt(playerPos);

            if (Vector3.Distance(e.transform.position, playerPos) < 0.5f)
            {
               // GameController.instance.GameOver();
            }
        }
    }
}
