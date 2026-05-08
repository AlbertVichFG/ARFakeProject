using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float speed = 1.5f;

    [SerializeField] private GameObject destroyEffect;

    void Update()
    {
        if (player == null) return;

        // direcció player
        Vector3 dir =
            (player.position - transform.position).normalized;

        // moure enemic
        transform.position +=
            dir * speed * Time.deltaTime;

        // mirar player
        transform.LookAt(player);
    }

    public void Die()
    {
        // efecte destrucció
        if (destroyEffect != null)
        {
            Instantiate(
                destroyEffect,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("MainCamera"))
        {
            GameController.instance.TakeDamage();

            Destroy(gameObject);
        }
    }
}
