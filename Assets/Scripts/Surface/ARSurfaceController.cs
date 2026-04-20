using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARSurfaceController : MonoBehaviour
{

    [SerializeField]
    private ARPlaneManager planeManager;
    [SerializeField] private ARRaycastManager raycastManager; // per detectar superfície i posar objecte

    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private int selectedPrefb = 0;

    private bool planeVisibility = true;
    private PlayerInput playerInput;

    [SerializeField]
    private GameObject canvasUI;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // per guardar resultats raycast

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // mostrar o amagar planes
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(planeVisibility);
            //plane.GetComponent<ARPlaneMeshVisualizer>().enabled = planeVisibility;
        }
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 touchPos = playerInput.actions["TouchPosition"].ReadValue<Vector2>();

            // raycast AR per detectar superfície
            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = hits[0].pose;

                Instantiate(prefabs[selectedPrefb], pose.position, pose.rotation);
            }
        }
    }

    public void ToggleVisibilityBttn()
    {
       // canvasUI.SetActive(!planeVisibility);

        planeVisibility = !planeVisibility;

    }

    // botons del menú per seleccionar objecte
    public void SelectPrefab(int index)
    {
        selectedPrefb = index;
    }
}
