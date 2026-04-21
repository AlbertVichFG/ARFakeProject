using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCameraImage : MonoBehaviour
{
    private WebCamTexture cam;

    [SerializeField] private RawImage backgroundTexture;
    [SerializeField] private TextMeshProUGUI proba;

    void Start()
    {
        WebCamDevice[] cams = WebCamTexture.devices;

        for (int i = 0; i < cams.Length; i++)
        {
            if (!cams[i].isFrontFacing)
            {
                cam = new WebCamTexture(cams[i].name);
                proba.text = "Camera: " + cams[i].name;
                break;
            }
        }

        if (cam == null)
        {
            proba.text = "No camera";
            return;
        }

        backgroundTexture.texture = cam;
        cam.Play();

        backgroundTexture.rectTransform.sizeDelta =
    new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        if (cam == null) return;

        // corregir rotació Android
        backgroundTexture.rectTransform.localEulerAngles =
            new Vector3(0, 0, -cam.videoRotationAngle);

        // corregir mirall
        backgroundTexture.rectTransform.localScale =
            new Vector3(1, cam.videoVerticallyMirrored ? -1 : 1, 1);


    }
}
