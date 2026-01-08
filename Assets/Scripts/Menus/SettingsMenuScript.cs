using UnityEngine;

public class SettingsMenuScript : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;

    [SerializeField] GameObject menu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (menuManager.activeMenu == null)
            {
                menu.SetActive(true);
                menuManager.activeMenu = menu;
            }
            else // if any other menu, then close it
            {
                menuManager.activeMenu.SetActive(false);
                menuManager.activeMenu = null;
            }
        }

    }
}
