using UnityEngine;
using UnityEngine.InputSystem;

public class GiroscopioController : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    private InputActionReference gyroAction;

    void Start()
    {
        Debug.Log("Start Gyro Script");

       /* if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }*/

        InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        InputSystem.EnableDevice(AttitudeSensor.current);
    }

    void Update()
    {
     //   if (!Input.gyro.enabled)
       //     Input.gyro.enabled = true;
    }


    void LateUpdate()
    {
      /*  if (SystemInfo.supportsGyroscope == true)
        {
           // Quaternion inputGyro = Input.gyro.attitude;

            Quaternion inputGyro = gyroAction.action.ReadValue<Quaternion>();
             

            //invertimos el eje z y w del quaternion para que la rotacion del
            //giroscopio encaje con la de la camara en coordenadas de unity
            //cam.rotation = new Quaternion(inputGyro.x, inputGyro.y, -inputGyro.z, -inputGyro.w);

            Quaternion correcionGiro = Quaternion.Euler(90, 0, 0);

            cam.rotation = correcionGiro * new Quaternion(inputGyro.x, inputGyro.y, -inputGyro.z, -inputGyro.w);

        }*/

      
        //Agafar valor de rotacio del mobil
        Vector3 gyroSpeed = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();
        cam.eulerAngles += gyroSpeed * -1; //potser toca ajustar algun eix


     /*   //Agafar valor del attitude
        Quaternion rot = AttitudeSensor.current.attitude.ReadValue();
        cam.rotation = rot; //potser toca ajustar algun eix*/

        //Acelerometro  
        Vector3 inputAcelerometro = Input.acceleration;
    }
}
