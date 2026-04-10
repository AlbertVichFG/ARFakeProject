using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrakImageController : MonoBehaviour
{

    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    [SerializeField] private GameObject framePrefab;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrakedChanged;
    }

    private void OnDisable()
    {
        
    }

    void OnTrakedChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {

            if (newImage.referenceImage.name == "FArt")
            {
                Instantiate(framePrefab, newImage.transform.position, newImage.transform.rotation);
            }
            
        }

        foreach (var newImage in eventArgs.removed)
        {
            //eliminar img
        }

        foreach (var newImage in eventArgs.updated)
        {
            //actualizar img
        }
    }
}
