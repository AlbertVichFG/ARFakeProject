using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 100;

    private int health;

    [SerializeField] private int minDamage = 10;
    [SerializeField] private int maxDamage = 25;

    [Header("Combat")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [Header("UI")]
    [SerializeField] private Image healthFill;

    [SerializeField] private TMP_Text damageText;

    private Animator animator;

    void Start()
    {
        health = maxHealth;

        animator = GetComponent<Animator>();

        UpdateHealthBar();

        damageText.gameObject.SetActive(false);
    }

    public void Attack(Fighter target)
    {
        if (!IsAlive()) return;

        animator.SetTrigger("Attack");

        int dmg =
            Random.Range(minDamage, maxDamage);

        // projectile
        if (projectilePrefab != null)
        {
            GameObject proj =
                Instantiate(
                    projectilePrefab,
                    shootPoint.position,
                    Quaternion.identity
                );

            proj.GetComponent<Projectile>()
                .Init(target.transform);
        }

        target.TakeDamage(dmg);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health < 0)
            health = 0;

        UpdateHealthBar();

        ShowDamage(dmg);

        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }
    }

    void UpdateHealthBar()
    {
        healthFill.fillAmount =
            (float)health / maxHealth;
    }

    void ShowDamage(int dmg)
    {
        damageText.gameObject.SetActive(true);

        damageText.text = "-" + dmg;

        CancelInvoke();

        Invoke(nameof(HideDamage), 1f);
    }

    void HideDamage()
    {
        damageText.gameObject.SetActive(false);
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
