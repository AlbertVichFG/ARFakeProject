using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        transform.LookAt(player);
        transform.rotation *= Quaternion.Euler(-90, 0, 0);


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            GameController.instance.GameOver();
        }
    }




}
