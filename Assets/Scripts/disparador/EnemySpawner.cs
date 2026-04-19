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
        Vector3 spawnPos = Camera.main.transform.position +
                           Camera.main.transform.forward * spawnDistance;

        GameObject prefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    private void MoveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject ene in enemies)
        {
            Vector3 playerPos = Camera.main.transform.position;

            Vector3 dir = (playerPos - ene.transform.position).normalized;

            ene.transform.position += dir * enemySpeed * Time.deltaTime;

            ene.transform.LookAt(playerPos);
        }
    }
}
