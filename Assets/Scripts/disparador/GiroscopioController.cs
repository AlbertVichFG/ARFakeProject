using UnityEngine;

public class GiroscopioController : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    void Start()
    {
        Input.gyro.enabled = true;
    }



    void LateUpdate()
    {
        if (SystemInfo.supportsGyroscope == true)
        {
            Quaternion inputGyro = Input.gyro.attitude;
            
            //invertimos el eje z y w del quaternion para que la rotacion del
            //giroscopio encaje con la de la camara en coordenadas de unity
            //cam.rotation = new Quaternion(inputGyro.x, inputGyro.y, -inputGyro.z, -inputGyro.w);

            Quaternion correcionGiro = Quaternion.Euler(90, 0, 0);

            cam.rotation = correcionGiro * new Quaternion(inputGyro.x, inputGyro.y, -inputGyro.z, -inputGyro.w);

        }
    }
}
