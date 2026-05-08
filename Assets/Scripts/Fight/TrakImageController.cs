using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrakImageController : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager manager;

    [SerializeField] private ARObjects[] arObjects;

    private Dictionary<string, GameObject> spawned =
        new Dictionary<string, GameObject>();

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

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        // noves imatges
        foreach (var img in args.added)
        {
            Spawn(img);
        }

        // update tracking
        foreach (var img in args.updated)
        {
            UpdateObject(img);
        }

        // eliminar tracking
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

        Debug.Log("Spawn image: " + name);

        // evitar duplicats
        if (spawned.ContainsKey(name))
            return;

        foreach (var ar in arObjects)
        {
            if (ar.referenceImageName == name)
            {
                GameObject obj =
                    Instantiate(ar.prefab, img.transform);

                spawned.Add(name, obj);

                Debug.Log("Prefab instantiated");

                // IMPORTANT
                Fighter fighter =
                    obj.GetComponentInChildren<Fighter>();

                if (fighter == null)
                {
                    Debug.LogError("NO FIGHTER FOUND");
                }
                else
                {
                    Debug.Log("Fighter found");
                }

                // assignar fighters
                if (fighter1 == null)
                {
                    fighter1 = fighter;

                    Debug.Log("fighter1 assigned");
                }
                else if (fighter2 == null)
                {
                    fighter2 = fighter;

                    Debug.Log("fighter2 assigned");
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

            obj.transform.position =
                img.transform.position;

            obj.transform.rotation =
                img.transform.rotation;
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
            Destroy(spawned[name]);

            spawned.Remove(name);
        }
    }

    void Update()
    {
        if (fighter1 != null)
            Debug.Log("fighter1 OK");

        if (fighter2 != null)
            Debug.Log("fighter2 OK");

        // si hi ha dos fighters
        if (fighter1 != null &&
            fighter2 != null)
        {
            Debug.Log("Both fighters detected");

            // mirar-se
            fighter1.transform.LookAt(fighter2.transform);

            fighter2.transform.LookAt(fighter1.transform);

            // començar combat
            if (!fightStarted)
            {
                Debug.Log("START FIGHT");

                fightStarted = true;

                StartCoroutine(FightLoop());
            }
        }
    }

    IEnumerator FightLoop()
    {
        Debug.Log("FightLoop started");

        yield return new WaitForSeconds(1f);

        while (fighter1.IsAlive() &&
               fighter2.IsAlive())
        {
            Debug.Log("fighter1 attacks");

            fighter1.Attack(fighter2);

            yield return new WaitForSeconds(1.5f);

            if (!fighter2.IsAlive())
                break;

            Debug.Log("fighter2 attacks");

            fighter2.Attack(fighter1);

            yield return new WaitForSeconds(1.5f);
        }

        Debug.Log("Fight ended");

        // guanyador
        if (fighter1.IsAlive())
        {
            Debug.Log("fighter1 wins");

            fighter1.Win();
        }
        else if (fighter2.IsAlive())
        {
            Debug.Log("fighter2 wins");

            fighter2.Win();
        }
    }
}

[System.Serializable]
public class ARObjects
{
    public string referenceImageName;

    public GameObject prefab;
}