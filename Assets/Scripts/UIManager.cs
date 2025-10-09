using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIManager : MonoBehaviour
{
    [HideInInspector] public bool inMenu = false;
    [HideInInspector] public bool inOverviewMenu = false;

    // In scene pull stuff
    public GameObject menues;
    public GameObject overviewMenu;
    public PlayerScript playerScript;
    public SaveJson saveJson;

    // Vars for items
    public ItemScript[] items;
    public ItemSlotScript[] itemSlots;

    // Hotbar stuff
    public int[] hotbarNums;


    public void getItems()
    {
        // Gets a list of all item slots
        int x = 0;

        clearItems();

        // Check each of the items slots to see if they have an item in them
        foreach (ItemSlotScript i in itemSlots)
        {
            //If it does have an item, it stores it into the items list at the proper index
            if (i.GetComponentInChildren<ItemScript>())
            {
                items[x] = i.GetComponentInChildren<ItemScript>();
            }
            Debug.Log(string.Join(", ", items[x]));
            x++;
        }
    }


    public void clearItems()
    {
        // Clears the item list before updating it
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }
    }


    public void Save()
    {
        getItems();
        saveJson.SaveInventoryData();


    }


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
        if (!inOverviewMenu && Input.GetKeyDown("tab"))
        {
            inOverviewMenu = true;
            MenuControl(overviewMenu);
        }
        else if (inOverviewMenu && Input.GetKeyDown(KeyCode.Tab))
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





    void SelectHotbar()
    {
        if (!inMenu)
        {
            foreach (int i in hotbarNums)
            {
                if (Input.GetKeyDown((i).ToString()))
                {
                    Debug.Log("i " + i);
                    itemSlots[i - 1].Selected();
                    Debug.Log("item.selected");
                }
            }
                
        }
            
    }


    private void Start()
    {
        #region Setup Variables
        saveJson = GetComponent<SaveJson>();

        #endregion
        


    }


    void Update()
    {
        CheckMenu();

        SelectHotbar();
    }
}
