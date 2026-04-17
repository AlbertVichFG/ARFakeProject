using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class ARSurfaceController : MonoBehaviour
{

    [SerializeField]
    private ARPlaneManager planeManager;

    [SerializeField]
    private GameObject prefab;
    private bool planeVisibility = true;
    private PlayerInput playerInput;

    [SerializeField]
    private GameObject canvasUI;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.GetComponent<ARPlaneMeshVisualizer>().enabled = planeVisibility;
        }
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

            Vector2 touchPos = playerInput.actions["TouchPosition"].ReadValue<Vector2>();

            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                
               if (EventSystem.current.IsPointerOverGameObject()) 
                    return;

                //Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                Instantiate(prefab, hit.point, Quaternion.identity);

            }
        }
    }

    public void ToggleVisibilityBttn()
    {
        canvasUI.SetActive(!planeVisibility);

        planeVisibility = !planeVisibility;

    }
}
