using TMPro;
using UnityEngine;

public class GyroDebug : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI Bool;

    void Start()
    {
      //  Input.gyro.enabled = true;
    }

    void Update()
    {
        text.text = "Supported: " + SystemInfo.supportsGyroscope +
        "\nEnabled: " + Input.gyro.enabled +
        "\nAttitude: " + Input.gyro.attitude;
        Bool.text =
           "Enabled: " + Input.gyro.enabled +
           "\nSupported: " + SystemInfo.supportsGyroscope; // Muestra si el giroscopio está habilitado*/
    }
}
