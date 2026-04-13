using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrakImageController : MonoBehaviour
{

    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    [SerializeField] 
    private ARObjects [] arObjects;

        private GameObject prefabCopy;

    private void OnEnable()
    {
       // trackedImageManager.trackedImagesChanged += OnTrakedChanged; obsolet

        trackedImageManager.trackablesChanged.AddListener(OnTrakedChanged);
    }

    private void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveListener(OnTrakedChanged);
    }

    void OnTrakedChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {

            for (int i = 0; i < arObjects.Length; i++)
            {
                if ( arObjects[i].referenceImageName == newImage.referenceImage.name )
                    {
                    prefabCopy = Instantiate(arObjects[i].prefab, newImage.transform.position, newImage.transform.rotation);
                }
            }

            if (newImage.referenceImage.name == "FArt")
            {
              // prefabCopy = Instantiate(framePrefab, newImage.transform.position, newImage.transform.rotation);
            }
            
        }

        foreach (var newImage in eventArgs.removed)
        {
            // eliminar img
            /*if (newImage.referenceImage.name == "FArt")
            {
                Destroy(prefabCopy);
            }*/
        }

        foreach (var newImage in eventArgs.updated)
        {
            //actualizar img
        }
    }
}

[Serializable]
public class ARObjects
{
    public string referenceImageName;
        public GameObject prefab;
}
