using UnityEngine;

public class ARObjectController : MonoBehaviour
{
    [SerializeField] private GameObject menuPrefab;

    private GameObject menuInstance;
    private int scaleState = 0;

    void Start()
    {
        // instanciar menú com a fill
        menuInstance = Instantiate(menuPrefab, transform);
        menuInstance.transform.localPosition = Vector3.up * 0.3f;

        menuInstance.SetActive(false);
    }

    public void ToggleMenu()
    {
        menuInstance.SetActive(!menuInstance.activeSelf);
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
}

