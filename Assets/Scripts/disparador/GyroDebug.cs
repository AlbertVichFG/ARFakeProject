using TMPro;
using UnityEngine;

public class GyroDebug : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI Bool;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        text.text = Input.gyro.attitude.ToString();
        Bool.text = Input.gyro.enabled.ToString(); // Muestra si el giroscopio está habilitado
    }
}
