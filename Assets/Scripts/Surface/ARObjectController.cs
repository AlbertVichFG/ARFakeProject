using UnityEngine;

public class ARObjectController : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    private int scaleState = 0;

    void Start()
    {
        menu.SetActive(false);
    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void ChangeScale()
    {
        scaleState++;

        if (scaleState == 1)
            transform.localScale = Vector3.one * 0.5f;

        else if (scaleState == 2)
            transform.localScale = Vector3.one * 2f;

        else
        {
            transform.localScale = Vector3.one;
            scaleState = 0;
        }
    }

    public void Rotate()
    {
        transform.Rotate(0, 90f, 0);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (menu != null)
        {
            menu.transform.LookAt(Camera.main.transform);
        }
    }
}

