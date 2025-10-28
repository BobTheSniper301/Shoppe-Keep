using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.IO;
using Unity.VisualScripting;
using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEditor;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{

    // In scene pull stuff
    public PlayerScript playerScript;
    
    public new Camera camera;

    [HideInInspector] public SaveJson saveJson;


    // Menu stuff
    [HideInInspector] public bool itemCanPlace = false;
    [HideInInspector] public bool inMenu = false;
    // Set in inspector first
    [SerializeField] GameObject menu;
    [SerializeField] GameObject overviewMenu;
    [SerializeField] GameObject containerText;
    [SerializeField] GameObject settingsMenu;


    // Vars for items
    // Set in inspector first
    [HideInInspector] public ItemData[] itemsData;

    [HideInInspector] public ItemScript[] items;

    [HideInInspector] public int[] hotbarNums;

    [HideInInspector] public ItemSlotScript[] itemSlots;

    [HideInInspector] public GameObject itemPickedUp;

    public GameObject selectedItem;

    [HideInInspector] public int lastItemSlotNum;


    // PREFABS
    [SerializeField] GameObject item;


    #region Items
    // Gets a list of all items
    public void GetItems()
    {

        clearItems();

        int i = 0;
        // Check each of the items slots to see if they have an item in them
        foreach (ItemSlotScript slot in itemSlots)
        {

            // Ensures itemslots show correct text for (non)stackable items
            slot.UpdateItemSlot();

            // If the itemslot does have an item, it stores it into the items list at the proper index
            if (slot.GetComponentInChildren<ItemScript>())
            {

                items[i] = slot.GetComponentInChildren<ItemScript>();
                itemsData[i] = items[i].itemData;

            }

            i++;
        }
        
    }


    // Gets a list of all item slots
    void GetItemSlots()
    {

        itemSlots = GetComponentsInChildren<ItemSlotScript>();

    }


    // Clears the item list before updating it
    public void clearItems()
    {

        for (int i = 0; i < items.Length; i++)
        {

            items[i] = null;
            itemsData[i] = null;
        }
    }


    // To deselect all itemslots
    void DeselectAll()
    {

        foreach (ItemSlotScript i in itemSlots)
        {
            i.Deselected();
        }
        selectedItem = null;
    }


    // Selects a hotbar slot depending upon input
    void SelectHotbar()
    {

        for (int i = 0; i < items.Length; i++)
            if (Input.GetKeyDown((i + 1).ToString()))
                {
                    // For select/deselect same slot
                    itemSlots[i].ToggleSelect();

                    DeselectAll();

                    if (!itemSlots[i].toggled)
                    {

                        itemSlots[i].Selected();

                    }

                }

    }


    // Clears all current item related data - items[], itemsData[], etc - and destroys all items in slots
    public void ClearInventory()
    {
        int i = 0;
        while (i < items.Length)
        {

            items[i] = null;
            itemsData[i] = null;
            itemSlots[i].GetComponentInChildren<Text>().text = "";
            if (itemSlots[i].GetComponentInChildren<ItemScript>() != null)
            {

                Destroy(itemSlots[i].GetComponentInChildren<ItemScript>().gameObject);

            }
            i++;

        }
    }


    // Adds a nonStackable item to the inventory in the first open slot (Adjusts transform values)
    public void AddItem(GameObject itemToAdd)
    {
        foreach (ItemSlotScript i in itemSlots)
        {
            if (i.GetComponentInChildren<ItemScript>() == null)
            {
                itemToAdd.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                itemToAdd.transform.SetParent(i.transform);
                itemToAdd.transform.localRotation = new Quaternion(0, 0, 0, 0);

                break;
            }
        }
    }


    // Adds a Stackable item to the inventory, will add to the count of the same stackable item if it's in the inventory
    // If a stackable item of the same name is not in, it will treat it as a normal item and add it into the first open slot
    public void AddStackableItem(GameObject itemToAdd)
    {
        bool didStack = false;
        foreach (ItemScript i in items)
        {

            if (i is ItemScript && itemToAdd.name == i.gameObject.name)
            {

                Destroy(itemToAdd);
                i.itemData.count += itemToAdd.GetComponent<ItemScript>().itemData.count;
                didStack = true;
                break;
            }
        }
        if (didStack == false)
        {
            AddItem(itemToAdd);
        }
    }


    // Called when an item is picked up; will do AddStackableItem() or AddItem() depending on type
    public void PickUpItem(GameObject itemToAdd)
    {
        if (itemToAdd.GetComponent<ItemScript>().itemData.itemType == ItemData.ItemType.STACKABLE)
        {

            AddStackableItem(itemToAdd);

        }
        else
        {

            AddItem(itemToAdd);

        }
        GetItems();
    }


    // Drops the selected item, goes forward from camera rotation, and adjusts transform values
    void DropItem()
    {
        if (selectedItem)
        {

            selectedItem.transform.rotation = camera.transform.rotation;
            selectedItem.transform.eulerAngles = new Vector3(0, selectedItem.transform.rotation.eulerAngles.y, selectedItem.transform.rotation.eulerAngles.z);

            selectedItem.transform.SetParent(null);

            selectedItem.transform.position = playerScript.gameObject.transform.position;
            selectedItem.transform.Translate(transform.forward * 5);

            selectedItem.transform.localScale = new Vector3(10, 10, 10);

            selectedItem.GetComponent<SphereCollider>().enabled = true;
        }
        DeselectAll();
        GetItems();
    }


    // Takes the selected item, moves it to the container, and adjusts transform values
    public void AddItemToContainer()
    {
        if (selectedItem != null)
        {
            selectedItem.transform.SetParent(playerScript.containerHit.transform.parent);
            selectedItem.transform.position = playerScript.containerHit.transform.parent.position;
            selectedItem.transform.rotation = playerScript.containerHit.transform.parent.rotation;
            selectedItem.transform.localScale = new Vector3(5, 5, 5);
            selectedItem.GetComponent<SphereCollider>().enabled = false;
        }
    }


    // For item placing; does proper item placing depending upon if theres an item in the container or not
    public void ItemInteract()
    {
        ItemScript itemInContainer = playerScript.containerHit.transform.parent.GetComponentInChildren<ItemScript>();
        if (itemInContainer != null)
        {
            Debug.Log("item in container");
            AddItemToContainer();
            DeselectAll();
            GetItems();
            PickUpItem(itemInContainer.gameObject);
        }
        else
        {
            AddItemToContainer();
        }
        DeselectAll();
        GetItems();
    }


    #endregion


    #region Menus 


    // Opens the given menu, and closes the rest. Will wipe all menus if a null value is passed
    void MenuOpen(GameObject keepMenu)
    {

        // All menus close
        overviewMenu.SetActive(false);
        containerText.SetActive(false);
        settingsMenu.SetActive(false);

        // Controls the in menu variable and freeing the player when leaving a menu
        if (keepMenu != null)
        {

            inMenu = true;
            keepMenu.gameObject.SetActive(true);

        }
        else
        {

            inMenu = false;
            menu.SetActive(false);
            playerScript.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    // Manages Container Text
    void ContainerText()
    {
        if (itemCanPlace)
        {
            containerText.SetActive(true);
            // Debug.Log("set active");
        }
        else if (containerText.activeSelf && !itemCanPlace)
        {
            // Debug.Log("else if");
            containerText.SetActive(false);
        }
    }

    // Manages Overview Menu
    void OverviewMenu()
    {

        if (Input.GetKeyDown("tab") && !overviewMenu.activeSelf)
        {

            MenuOpen(overviewMenu);

        }
        else if (overviewMenu.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {

            MenuOpen(null);

        }

    }

    // Manages Settings Menu
    public void SettingsMenu()
    {
        MenuOpen(settingsMenu);
    }


    public void QuitToDesktop()
    {
        Save();
        Application.Quit();
    }


    public void QuitToMainMenu()
    {
        Save();
        SceneManager.LoadScene("Start");
    }





    // Checks if the player is in a menu, if so, the player can no longer do anything, locks mouse, opens menus
    void CheckMenu()
    {
        OverviewMenu();
        ContainerText();

        if (inMenu)
        {
            menu.gameObject.SetActive(true);
            playerScript.canMove = false;
            Cursor.lockState = CursorLockMode.None;
        }
        if (inMenu && Input.GetKeyDown("escape"))
        {
            MenuOpen(null);
        }
    }

    #endregion


    #region Save + Load

    public void Save()
    {
        // Updates items list then saves the data of the list and each item's data
        #region Inventory Save

        GetItems();

        saveJson.SaveInventoryData();

        #endregion

    }


    public void Load()
    {

        // Gets the saved inventory data; Creates all the new items with the appropriate data, transform, etc
        #region Inventory Load

        ClearInventory();

        saveJson.LoadInventoryData();

        for (int i = 0; i < itemsData.Length; i++)
        {

            // If it's not a blank item, make the item
            if (itemsData[i] != null && itemsData[i].itemName != "null")
            {

                GameObject newItem = Instantiate(item, itemSlots[i].transform);
                newItem.GetComponent<ItemScript>().itemData = itemsData[i];
                newItem.GetComponent<ItemScript>().itemData.name = itemsData[i].itemName;
                newItem.name = itemsData[i].itemName;
                newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImages/" + newItem.name);
                newItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ItemImages/" + newItem.name);
                items[i] = newItem.GetComponent<ItemScript>();
            }

        }

        // For some reason has to happen after the load function happens to properly get the right items
        Invoke("GetItems", 0.001f);

        #endregion

    }

    #endregion


    #region Function Calls

    void Awake()
    {

        saveJson = GetComponent<SaveJson>();
        
    }


    void Start()
    {
        GetItemSlots();

        GetItems();

        // TODO: Uncomment the load (works better when commented for testing)
        //Load();

    }


    void Update()
    {
        CheckMenu();

        // Hotbar Selection
        if (!inMenu)
        {

            SelectHotbar();

        }

        // Item Drop
        if (Input.GetKeyDown("g"))
        {

            DropItem();

        }

        // Item Placing
        if (itemCanPlace && Input.GetKeyDown("f"))
        {
            ItemInteract();
        }
    }
    
    #endregion
}
