using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

   // [SerializeField] private GameObject destroyEffect;

    private Transform player;

    void Start()
    {
        // càmera real jugador
        player = Camera.main.transform;
    }

    void Update()
    {
        if (player == null) return;

        // direcció cap player
        Vector3 dir =
            (player.position - transform.position).normalized;

        // moviment
        transform.position +=
            dir * speed * Time.deltaTime;

        // mirar player
        Vector3 targetPos = player.position;

        targetPos.y = transform.position.y;

        transform.LookAt(targetPos);
        transform.rotation *= Quaternion.Euler(-90f, 0f, 0f);
    }

    public void Die()
    {
      /*  if (destroyEffect != null)
        {
            Instantiate(destroyEffect,transform.position,Quaternion.identity );
        }*/

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") ||
            other.CompareTag("Player"))
        {
            GameController.instance.TakeDamage();

            Destroy(gameObject);
        }
    }
}
