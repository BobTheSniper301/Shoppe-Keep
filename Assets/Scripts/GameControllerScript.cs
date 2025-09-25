using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [HideInInspector] public bool inMenu = false;
    [HideInInspector] public bool inOverviewMenu = false;


    // Ui vars
    public GameObject menues;
    public GameObject overviewMenu;
    

    public PlayerScript playerScript;


    


    // Controls what menus open and which ones are closed. Will wipe all menus if a null value is passed
    void MenuControl(GameObject keepMenu)
    {
        // All other menus to close
        overviewMenu.gameObject.SetActive(false);
        
        
        if (keepMenu != null)
        {
            inMenu = true;
            keepMenu.gameObject.SetActive(true);
        }
        else
        {
            inMenu = false;
            menues.gameObject.SetActive(false);
            playerScript.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    void OverviewMenu()
    {
        if (!inOverviewMenu && Input.GetKeyDown(KeyCode.Tab))
        {
            inOverviewMenu = true;
            MenuControl(overviewMenu);
        }
        else if(inOverviewMenu && Input.GetKeyDown(KeyCode.Tab))
        {
            inOverviewMenu = false;
            MenuControl(null);
        }
    }

    void CheckMenu()
    {
        OverviewMenu();
        
        if (inMenu)
        {
            menues.gameObject.SetActive(true);
            playerScript.canMove = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    void Update()
    {
        CheckMenu();
        


    }
}
