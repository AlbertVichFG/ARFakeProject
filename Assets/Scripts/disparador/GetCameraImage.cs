using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCameraImage : MonoBehaviour
{
    private WebCamTexture cam;
    [SerializeField]
    private RawImage backgroundTexture;
    [SerializeField] private TextMeshProUGUI proba;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //primero: revisar camaras de nuestro dispositivo
        WebCamDevice[] realCamaras = WebCamTexture.devices;

        proba.text = "Hola?";


        for (int i = 0; i < realCamaras.Length; i++)
        {
            proba.text = "Camara trasera encontrada: " + realCamaras[i].name;


            Debug.Log(realCamaras[i].name);
            if (realCamaras[i].isFrontFacing == false)
            {
                cam = new WebCamTexture(realCamaras[i].name, Screen.width, Screen.height);
                proba.text = "Camara trasera encontrada: " + realCamaras[i].name;
            }

        }

       

        cam.Play();
        backgroundTexture.texture = cam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
