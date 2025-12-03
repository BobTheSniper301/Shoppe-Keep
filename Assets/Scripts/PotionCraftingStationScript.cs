using UnityEngine;

public class PotionCraftingStationScript : MonoBehaviour
{
    
    public void OpenCrafting()
    {
        UiManager.instance.canOpenMenu = false;
        PlayerScript.instance.canMove = false;
        UiManager.instance.inMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("opencrafting");
        UiManager.instance.craftingMenu.SetActive(true);
    }


    public void QuitCrafting()
    {
        UiManager.instance.canOpenMenu = true;
        PlayerScript.instance.canMove = true;
        UiManager.instance.inMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("quitcrafting");
        UiManager.instance.craftingMenu.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            QuitCrafting();
            UiManager.instance.MenuOpen(null);
        }
    }
}
