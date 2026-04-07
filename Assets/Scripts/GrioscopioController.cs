using UnityEngine;

public class GrioscopioController : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Quaternion inputGyro = Input.gyro.attitude;

           // cam.rotation = new Quaternion(inputGyro.x, inputGyro.y, -inputGyro.z, -inputGyro.w);

            Quaternion correctionGir = Quaternion.Euler(90, 0, 0);

            cam.rotation = correctionGir * new Quaternion(inputGyro.x, inputGyro.y, -inputGyro.z, -inputGyro.w);

        }
    }
}
