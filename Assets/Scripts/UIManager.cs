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
    //Set in inspector first
    [HideInInspector] public ItemData[] itemsData;

    [HideInInspector] public ItemScript[] items;

    [HideInInspector] public int[] hotbarNums;

    [HideInInspector] public ItemSlotScript[] itemSlots;

    [HideInInspector] public GameObject itemPickedUp;

    public GameObject selectedItem;

    [HideInInspector] public int lastItemSlotNum;

    

    //PREFABS
    [SerializeField] GameObject item;


    #region Items
    // Gets a list of all items
    public void GetItems()
    {
        // Debug.Log("getItems");

        clearItems();

        int i = 0;
        // Check each of the items slots to see if they have an item in them
        foreach (ItemSlotScript slot in itemSlots)
        {

            slot.UpdateItemSlot();

            // If it does have an item, it stores it into the items list at the proper index
            if (slot.GetComponentInChildren<ItemScript>())
            {

                items[i] = slot.GetComponentInChildren<ItemScript>();
                itemsData[i] = items[i].itemData;

            }

            // Debug.Log(string.Join(", ", items[x]));
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


    // To deselect any/all itemslots
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


    public void ClearInventory()
    {
        int i = 0;
        while (i < items.Length)
        {

            // itemsData[i] = ScriptableObject.CreateInstance<ItemData>();
            // itemSlots[i].GetComponentInChildren<Text>().text = "";
            // items[i] = null;
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


    public void ItemInteract()
    {
        Debug.Log("place");
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
            Debug.Log("no Item in container");
            if (selectedItem != null)
            {
                // Debug.Log(playerScript.containerHit.transform.parent.name);
                AddItemToContainer();
            }
        }
        DeselectAll();
        GetItems();
    }


    #endregion



    #region Menus 


    // Controls what menus open and which ones are closed. Will wipe all menus if a null value is passed
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


    public void SettingsMenu()
    {
        MenuOpen(settingsMenu);
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



    public void Save()
    {
        #region Inventory Save
        GetItems();
        saveJson.SaveInventoryData();

        //for (int i = 0; i < items.Length; i++)
        //{
        //    Debug.Log(items[i]);
        //}

        #endregion

    }


    public void Load()
    {
        #region Inventory Load

        ClearInventory();

        saveJson.LoadInventoryData();

        int i = 0;
        while (i < items.Length)
        {

            if (items[i] is ItemScript)
            {

                items[i].gameObject.transform.SetParent(itemSlots[i].gameObject.transform);

            }

            i++;
        }

        for (int j = 0; j < itemsData.Length; j++)
        {

            if (itemsData[j].itemName != "null")
            {

                GameObject newItem = Instantiate(item, itemSlots[j].transform);
                newItem.GetComponent<ItemScript>().itemData = itemsData[j];
                newItem.GetComponent<ItemScript>().itemData.name = itemsData[j].itemName;
                newItem.name = itemsData[j].itemName;
                newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImages/" + newItem.name);
                newItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ItemImages/" + newItem.name);
                items[j] = newItem.GetComponent<ItemScript>();
                // Debug.Log(newItem.name);
            }

        }

        Invoke("GetItems", 0.001f);

        #endregion

    }


    #region Function Calls

    void Awake()
    {

        saveJson = GetComponent<SaveJson>();
        
    }


    void Start()
    {
        GetItemSlots();

        GetItems();

        //Load();

    }


    void Update()
    {
        CheckMenu();

        if (!inMenu)
        {

            SelectHotbar();

        }

        if (Input.GetKeyDown("g"))
        {

            DropItem();

        }

        if (itemCanPlace && Input.GetKeyDown("f"))
        {
            ItemInteract();
        }
    }
    
    #endregion
}
