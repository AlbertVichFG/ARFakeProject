using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public static float enemySpeed = .4f;

    private Transform player;
  //  [SerializeField] GameObject explosionPrefab;

    void Start()
    {
        // efecte spawn
       // transform.localScale = Vector3.one;

        player = Camera.main.transform;
        //   StartCoroutine(SpawnPop());
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;

        transform.position += dir * enemySpeed * Time.deltaTime;

        transform.LookAt(player);
    }

  /*  IEnumerator SpawnPop()
    {
        float t = 0;
        float duration = 0.2f;

        while (t < duration)
        {
            float scale = Mathf.Lerp(0, 1, t / duration);
            transform.localScale = Vector3.one * scale;

            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;
    }*/

   /* public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            GameController.instance.GameOver();
        }
    }




}
