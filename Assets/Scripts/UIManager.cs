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
    [HideInInspector] public ItemScript[] items;
    [HideInInspector] public ItemSlotScript[] itemSlots;
    [HideInInspector] public int lastItemSlotNum;

    // Hotbar stuff
    public int[] hotbarNums;


    #region Items
    // Gets a list of all items
    public void getItems()
    {

        int x = 0;

        clearItems();

        // Check each of the items slots to see if they have an item in them
        foreach (ItemSlotScript i in itemSlots)
        {
            i.updateSlottedItem();
            //If it does have an item, it stores it into the items list at the proper index
            if (i.GetComponentInChildren<ItemScript>())
            {
                items[x] = i.GetComponentInChildren<ItemScript>();
            }
            // Debug.Log(string.Join(", ", items[x]));
            x++;
        }
    }

    // Gets a list of all item slots
    void getItemSlots()
    {
        itemSlots = GetComponentsInChildren<ItemSlotScript>();
    }


    // Clears the item list before updating it
    public void clearItems()
    {

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }
    }


    // To deselect any/all itemslots
    void DeselectAll()
    {
        foreach (ItemSlotScript i in itemSlots)
            i.Deselected();
    }


    // Selects a hotbar slot depending upon input
    void SelectHotbar()
    {
        if (!inMenu)
        {
            foreach (int i in hotbarNums)
            {
                if (Input.GetKeyDown((i).ToString()))
                {
                    itemSlots[i - 1].ToggleSelect();

                    DeselectAll();

                    if (!itemSlots[i - 1].toggled)
                    {
                        itemSlots[i - 1].Selected();
                    }

                }
            }
                
        }
            
    }
    #endregion


    #region Menus 

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

    // Checks if the play is in a menu, if so, the player can no longer do anything, locks mouse, opens menus
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

    #endregion


    public void Save()
    {
        #region Inventory Save
        getItems();
        saveJson.SaveInventoryData();

        #endregion

    }


    // public void Load()
    // {
    //     #region Inventory Load
    //     Debug.Log("load");
    //     saveJson.LoadInventoryData();

    //     getItems();

    //     #endregion

    // }


    private void Start()
    {
        #region Setup Variables
        saveJson = GetComponent<SaveJson>();

        #endregion

        getItemSlots();

        // Load();
    }


    void Update()
    {
        CheckMenu();

        SelectHotbar();

        
    }
}
