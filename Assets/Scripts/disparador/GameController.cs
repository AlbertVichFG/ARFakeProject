using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private PlayerInput input;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject warningLeft;
    [SerializeField] private GameObject warningRight;

    private int score;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        input = GetComponent<PlayerInput>();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        CheckWarnings();
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
            return;

        Vector2 pos = input.actions["TouchPosition"].ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
                score++;

                scoreText.text = "Score: " + score;
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        gameOverPanel.SetActive(true);

        finalScoreText.text = "Score: " + score;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    void CheckWarnings()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        bool left = false;
        bool right = false;

        foreach (GameObject e in enemies)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(e.transform.position);

            if (pos.z < 0) continue;

            if (pos.x < 0) left = true;
            if (pos.x > 1) right = true;
        }

        warningLeft.SetActive(left);
        warningRight.SetActive(right);
    }
}
