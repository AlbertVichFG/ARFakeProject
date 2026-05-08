using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class TrakImageController : MonoBehaviour
{
    [Header("AR")]
    [SerializeField] private ARTrackedImageManager manager;

    [SerializeField] private ARObjects[] arObjects;

    private Dictionary<string, GameObject> spawned =
        new Dictionary<string, GameObject>();




    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

    private bool paused = false;


    private Fighter fighter1;
    private Fighter fighter2;

    private bool fightStarted = false;

    void OnEnable()
    {
        manager.trackablesChanged.AddListener(OnChanged);
    }

    void OnDisable()
    {
        manager.trackablesChanged.RemoveListener(OnChanged);
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var img in args.added)
        {
            Spawn(img);
        }

        foreach (var img in args.updated)
        {
            UpdateObject(img);
        }

        foreach (var img in args.removed)
        {
          //  Remove(img);
        }
    }

    void Spawn(ARTrackedImage img)
    {
        if (img.referenceImage == null)
            return;

        string name = img.referenceImage.name;

        if (string.IsNullOrEmpty(name))
            return;

        if (spawned.ContainsKey(name))
            return;

        foreach (var ar in arObjects)
        {
            if (ar.referenceImageName == name)
            {
                GameObject obj =
                    Instantiate(ar.prefab, img.transform);

                spawned.Add(name, obj);

                Fighter fighter =
                    obj.GetComponentInChildren<Fighter>();

                if (fighter1 == null)
                {
                    fighter1 = fighter;
                }
                else if (fighter2 == null)
                {
                    fighter2 = fighter;
                }
            }
        }
    }

    void UpdateObject(ARTrackedImage img)
    {
        if (img.referenceImage == null)
            return;

        string name = img.referenceImage.name;

        if (string.IsNullOrEmpty(name))
            return;

        if (spawned.ContainsKey(name))
        {
            GameObject obj = spawned[name];

            obj.SetActive(true);

            obj.transform.position = img.transform.position;

            obj.transform.rotation = img.transform.rotation;
        }
    }

    void Remove(ARTrackedImage img)
    {
        if (img.referenceImage == null)
            return;

        string name = img.referenceImage.name;

        if (string.IsNullOrEmpty(name))
            return;

        if (spawned.ContainsKey(name))
        {
            spawned[name].SetActive(false);
        }
    }

    void Update()
    {
        if (fighter1 != null &&
            fighter2 != null)
        {
            fighter1.transform.LookAt(fighter2.transform);

            fighter2.transform.LookAt(fighter1.transform);

            if (!fightStarted)
            {
                fightStarted = true;

                StartCoroutine(FightLoop());
            }
        }
    }

    IEnumerator FightLoop()
    {
        yield return new WaitForSeconds(1f);

        while (fighter1.IsAlive() &&
               fighter2.IsAlive())
        {
            fighter1.Attack(fighter2);

            yield return new WaitForSeconds(1.5f);

            if (!fighter2.IsAlive())
                break;

            fighter2.Attack(fighter1);

            yield return new WaitForSeconds(1.5f);
        }

        // winner
        if (fighter1.IsAlive())
        {
            fighter1.Win();

            ShowGameOver(fighter1);
        }
        else if (fighter2.IsAlive())
        {
            fighter2.Win();

            ShowGameOver(fighter2);
        }
    }

    void ShowGameOver(Fighter winner)
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        manager.enabled = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;

        manager.enabled = false;

        SceneManager.LoadScene(0);
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
}

[System.Serializable]
public class ARObjects
{
    public string referenceImageName;

    public GameObject prefab;
}