using UnityEngine;
using UnityEngine.UI;

public class GetCameraImage : MonoBehaviour
{
    private WebCamTexture cam;
    [SerializeField]
    private RawImage backgroundTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //primero: revisar camaras de nuestro dispositivo
        WebCamDevice[] realCamaras = WebCamTexture.devices;

        for(int i = 0; i < realCamaras.Length; i++)
        {
            Debug.Log(realCamaras[i].name);
            if (realCamaras[i].isFrontFacing == false)
            {
                cam = new WebCamTexture(realCamaras[i].name, Screen.width, Screen.height);
            }

        }
        //Esto es para que no me de error en unity
        cam = new WebCamTexture(realCamaras[0].name, Screen.width, Screen.height);

        cam.Play();
        backgroundTexture.texture = cam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
