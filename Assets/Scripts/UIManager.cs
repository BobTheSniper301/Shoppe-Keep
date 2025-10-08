using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIManager : MonoBehaviour
{
    [HideInInspector] public bool inMenu = false;
    [HideInInspector] public bool inOverviewMenu = false;

    public GameObject menues;
    public GameObject overviewMenu;


    public PlayerScript playerScript;

    public ItemScript[] items;
    public ItemSlotScript[] itemSlots;
    public SaveJson saveJson;


    public void getItems()
    {
        // Gets a list of all item slots
        int x = 0;
        //itemSlots = GetComponentsInChildren<ItemSlotScript>();

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
        GetComponent<SaveJson>().SaveInventoryData();


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
        if (!inOverviewMenu && Input.GetKeyDown(KeyCode.Tab))
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


    void Update()
    {
        CheckMenu();
    }
}
