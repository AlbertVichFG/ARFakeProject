using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private PlayerInput playerInput;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreText;

    private int score;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gameOverPanel.SetActive(false);
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
            return;

        Vector2 touchPos =
            playerInput.actions["TouchPosition"].ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
                score++;
            }
        }
    }

    public void GameOver()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }

        scoreText.text = "Score: " + score;

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
