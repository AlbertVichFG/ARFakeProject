using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 8f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float enemySpeed = 1.5f;

    private float timer;

    void Start()
    {
        // cada 10 segons augmenta dificultat
        InvokeRepeating(nameof(IncreaseDifficulty), 10f, 10f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }

        MoveEnemies();
    }

    void SpawnEnemy()
    {
        // rang perquè no surtin al centre
        float horizontal = Random.Range(-4f, 4f);
        float vertical = Random.Range(-1f, 2f);

        Vector3 spawnPos =
            player.position +
            player.forward * spawnDistance +
            new Vector3(horizontal, vertical, 0);

        GameObject prefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);

        // efecte spawn (pop)
        enemy.transform.localScale = Vector3.zero;
        StartCoroutine(ScaleUp(enemy.transform));
    }

    void MoveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector3 playerPos = player.position;

            Vector3 dir = (playerPos - enemy.transform.position).normalized;

            enemy.transform.position += dir * enemySpeed * Time.deltaTime;

            enemy.transform.LookAt(playerPos);

            // si arriba  game over
            if (Vector3.Distance(enemy.transform.position, playerPos) < 0.5f)
            {
                GameController.instance.GameOver();
            }
        }
    }

    void IncreaseDifficulty()
    {
        spawnInterval -= 0.1f;
        enemySpeed += 0.2f;

        if (spawnInterval < 0.5f)
            spawnInterval = 0.5f;
    }


    // efecte spawn (pop)
    IEnumerator ScaleUp(Transform t)
    {
        float time = 0;
        float duration = 0.2f;

        while (time < duration)
        {
            float scale = Mathf.Lerp(0, 1, time / duration);
            t.localScale = Vector3.one * scale;

            time += Time.deltaTime;
            yield return null;
        }

        t.localScale = Vector3.one;
    }
}
