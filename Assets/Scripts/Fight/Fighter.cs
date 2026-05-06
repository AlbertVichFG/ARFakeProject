using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int minDamage = 10;
    [SerializeField] private int maxDamage = 25;

     private Animator animator;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack(Fighter target)
    {
        if (!IsAlive()) return;

        animator.SetTrigger("Attack");

        int dmg = Random.Range(minDamage, maxDamage);

        // projectil
        if (projectilePrefab != null)
        {
            GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            proj.GetComponent<Projectile>().Init(target.transform);
        }

        target.TakeDamage(dmg);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }
    }

    public void Win()
    {
        animator.SetTrigger("Dance");
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
