using UnityEngine;

public class OverviewMenuScript : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;

    [SerializeField] GameObject menu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            if (menuManager.activeMenu == menu)
            {
                menu.SetActive(false);
                menuManager.activeMenu = null;
                return;
            }
            else if (menuManager.activeMenu != null)
            {
                menuManager.activeMenu.SetActive(false);
            }
            menu.SetActive(true);
            menuManager.activeMenu = menu;
        }
        
    }
}
