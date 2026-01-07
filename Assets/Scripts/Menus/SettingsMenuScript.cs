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
            Debug.Log(menu.name + " key");
            if (menuManager.activeMenu == null)
            {
                Debug.Log("null");
                menu.SetActive(true);
                menuManager.activeMenu = menu;
            }
            else if (menuManager.activeMenu == menu)
            {
                Debug.Log("this");
                menu.SetActive(false);
                menuManager.activeMenu = null;
            }
            else
            {
                Debug.Log("else");
                menuManager.activeMenu.SetActive(false);
                menuManager.activeMenu = null;
            }
        }

    }
}
