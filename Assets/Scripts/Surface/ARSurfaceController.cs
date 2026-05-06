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
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(planeVisibility);
        }
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 touchPos = playerInput.actions["TouchPosition"].ReadValue<Vector2>();

        // mirar si cliques un objecte existent
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            ARObjectController obj = hit.collider.GetComponent<ARObjectController>();

            if (obj != null)
            {
                obj.ToggleMenu();
                return;
            }
        }

        // instanciar
        if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            GameObject obj = Instantiate(prefabs[selectedPrefb], pose.position, pose.rotation);


        }
    }

    public void ToggleVisibilityBttn()
    {
        planeVisibility = !planeVisibility;
    }

    public void SelectPrefab(int index)
    {
        selectedPrefb = index;
    }
}
