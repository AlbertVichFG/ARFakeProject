using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Game1()
    {
        SceneManager.LoadScene(1);
    }

    public void Game2()
    {
        SceneManager.LoadScene(2);
    }

    public void Game3()
    {
        SceneManager.LoadScene(3);
    }
}
