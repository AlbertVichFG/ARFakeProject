using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public static float speed = 1.5f;

    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
    }

    void Update()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        transform.LookAt(player);
        transform.Rotate(90f, 0f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            GameController.instance.GameOver();
        }
    }

}
