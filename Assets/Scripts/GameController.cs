using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        //Input manager
        /*if (Input.touchCount> 0)
        {
            Touch  touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began) //primer frame que dit toca pantalla
            {

            }


          /*  if(touch.phase == TouchPhase.Moved) //detecta dit esta en posicio difente al frame anterior
            {
    
            }
    
            if(touch.phase == TouchPhase.Stationary) //mira si dit esta mateixa poscio que frame anterior
            {

            }

            if(touch.phase == TouchPhase.Ended) //detecta que dit ha deixat de tocar pantalla
            {

            }

            if(touch.phase == TouchPhase.Canceled) //detecta que el sistema ha cancelat el touch
            {

            }

            //touch.position; //posicio del touch en pantalla*/



        //input system MILLOR!!
    }



    public void TouchScreen(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            Vector2 touchPosition = playerInput.actions["TouchPositon"].ReadValue<Vector2>();

        }

        /*if(context.phase == InputActionPhase.Performed)
        {

        }

        if(context.phase == InputActionPhase.Canceled)
        {

        }*/
    }
}
