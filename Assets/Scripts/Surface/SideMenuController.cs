using UnityEngine;
using UnityEngine.SceneManagement;

public class SideMenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

    private bool paused = false;

    private bool open = true;


    private void Start()
    {
        pausePanel.SetActive(false);

    }

    public void ToggleMenu()
    {
        open = !open;

        menu.SetActive(open);
    }

    public void TogglePause()
    {
        paused = !paused;

        pausePanel.SetActive(paused);

        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }


    public void Resume()
    {
        paused = false;

        pausePanel.SetActive(false);

        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
