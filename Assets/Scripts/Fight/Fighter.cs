using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    [Header("SFX")]

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip attackSFX;


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

        int dmg = Random.Range(minDamage, maxDamage);

        // SFX
        if (attackSFX != null)
        {
            audioSource.PlayOneShot(attackSFX);
        }

        // projectile
        if (projectilePrefab != null)
        {
            GameObject proj =Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            proj.GetComponent<Projectile>().Init(target, dmg);
        }

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
        healthFill.fillAmount = (float)health / maxHealth;
    }

    void ShowDamage(int dmg)
    {
        damageText.gameObject.SetActive(true);

        damageText.text = "-" + dmg;

        // gradient
        float t = Mathf.InverseLerp(minDamage, maxDamage, dmg);

        damageText.color =Color.Lerp(Color.yellow, Color.red, t);

        StartCoroutine(DamagePop());

        CancelInvoke();

        Invoke(nameof(HideDamage), 1f);
    }

    IEnumerator DamagePop()
    {
        damageText.transform.localScale = Vector3.one * 1.5f;

        yield return new WaitForSeconds(0.1f);

        damageText.transform.localScale = Vector3.one;
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
