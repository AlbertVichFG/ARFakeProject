using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input manager
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)//El primer frame que el dedo toca la pantalla
            {

            }
            /*
            if(touch.phase == TouchPhase.Moved)//detecta si el dedo esta en una posicion distinta del frame anterior
            {

            }

            if(touch.phase == TouchPhase.Stationary)// que mira si el dedo esta en la misma posicion que el frame anterior
            {

            }

            if( touch.phase == TouchPhase.Ended)//el frame despues de que el dedo haya dejado de tocar la pantalla
            {

            }

            if(touch.phase == TouchPhase.Canceled) //el frame despues de que el dedo haya dejado de tocar la pantalla
            {

            }
            
            //touch.position Es la posicion en pixeles de la pantalla del dedo
        }*/


        //Input system

    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Vector2 touchPos= playerInput.actions["TouchPosition"].ReadValue<Vector2>();
        }

        /*if(context.phase == InputActionPhase.Performed)
        {

        }

        if (context.phase == InputActionPhase.Canceled)
        {

        }*/
    }
}
