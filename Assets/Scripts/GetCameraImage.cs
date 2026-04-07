using UnityEngine;
using UnityEngine.UI;

public class GetCameraImage : MonoBehaviour
{
    private WebCamTexture cam;

    [SerializeField]
    private RawImage backgroundTexture;

    void Start()
    {
        //primera revisar cameras dispositiu
        WebCamDevice[] realCameras = WebCamTexture.devices;

        for (int i = 0; i < realCameras.Length; i++)
        {
            Debug.Log(realCameras[i].name);
            if (!realCameras[i].isFrontFacing) 
            {
                cam = new WebCamTexture(realCameras[i].name, Screen.width, Screen.height);
            }
        }


       // cam = new WebCamTexture( realCameras[0].name, Screen.width, Screen.height);
        cam.Play();
        backgroundTexture.texture = cam;
    }

    void Update()
    {
        
    }
}
