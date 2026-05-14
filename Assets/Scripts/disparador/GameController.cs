using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private PlayerInput input;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private TMP_Text finalScoreText;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private LineRenderer laser;

    private int score = 0;

    // vides player
    [SerializeField]
    private int lives = 3;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        input = GetComponent<PlayerInput>();

        gameOverPanel.SetActive(false);

        UpdateUI();
    }

    // click pantalla
    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
            return;

        Vector2 pos =
            input.actions["TouchPosition"].ReadValue<Vector2>();

        Ray ray =
            Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            EnemyController enemy =  hit.collider.GetComponent<EnemyController>();

            if (enemy != null)
            {
                StartCoroutine(
                    ShootLaser(ray.origin, hit.point)
                );

                enemy.Die();

                score++;

                UpdateUI();
            }
        }
    }

    IEnumerator ShootLaser(Vector3 start, Vector3 end)
    {
        laser.enabled = true;

        laser.SetPosition(0, start);
        laser.SetPosition(1, end);

        yield return new WaitForSeconds(0.05f);

        laser.enabled = false;
    }

    public void TakeDamage()
    {
        lives--;

        UpdateUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
                hearts[i].sprite = fullHeart;

            else
                hearts[i].sprite = emptyHeart;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        gameOverPanel.SetActive(true);

        finalScoreText.text ="Final Score: " + score;
    }

    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void Resume()
    {
        Time.timeScale = 1;

        pausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }
}
