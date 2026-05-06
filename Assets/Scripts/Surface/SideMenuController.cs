using UnityEngine;

public class SideMenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    private bool open = true;

    public void ToggleMenu()
    {
        open = !open;

        menu.SetActive(open);
    }
}
