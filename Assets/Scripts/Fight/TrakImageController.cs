using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrakImageController : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager manager;
    [SerializeField] private ARObjects[] arObjects;

    private Dictionary<string, GameObject> spawned = new Dictionary<string, GameObject>();

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
            Remove(img);
        }
    }

    private void Remove(KeyValuePair<TrackableId, ARTrackedImage> img)
    {
        throw new NotImplementedException();
    }

    void Spawn(ARTrackedImage img)
    {
        string name = img.referenceImage.name;

        if (spawned.ContainsKey(name)) return;

        foreach (var ar in arObjects)
        {
            if (ar.referenceImageName == name)
            {
                GameObject obj = Instantiate(ar.prefab, img.transform);
                spawned.Add(name, obj);
            }
        }
    }

    void UpdateObject(ARTrackedImage img)
    {
        string name = img.referenceImage.name;

        if (spawned.ContainsKey(name))
        {
            GameObject obj = spawned[name];

            obj.transform.position = img.transform.position;
            obj.transform.rotation = img.transform.rotation;
        }
    }

    void Remove(ARTrackedImage img)
    {
        string name = img.referenceImage.name;

        if (spawned.ContainsKey(name))
        {
            Destroy(spawned[name]);
            spawned.Remove(name);
        }
    }
}

[System.Serializable]
public class ARObjects
{
    public string referenceImageName;
    public GameObject prefab;
}