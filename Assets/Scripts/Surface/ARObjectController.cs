using UnityEngine;

public class ARObjectController : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    private int scaleState = 0;

    private Vector3 defaultScale;

    void Start()
    {
        menu.SetActive(false);

        defaultScale = transform.localScale;
    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void ChangeScale()
    {
        Debug.Log("Change Scale");

        scaleState++;

        if (scaleState == 1)
        {
            // doble mida
            transform.localScale = defaultScale * 2f;
        }
        else if (scaleState == 2)
        {
            // meitat mida
            transform.localScale = defaultScale * 0.5f;
        }
        else
        {
            // mida normal
            transform.localScale = defaultScale;

            scaleState = 0;
        }
    }

    public void Rotate()
    {
        Debug.Log("Rotate");
        transform.Rotate(0, 90f, 0);
    }

    public void Delete()
    {
        Debug.Log("Delete");

        Destroy(gameObject);
    }

    /*void Update()
    {
        if (menu != null)
        {
            menu.transform.LookAt(Camera.main.transform);
        }
    }*/
}

