using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.IO;
using Unity.VisualScripting;
using System;
using UnityEditorInternal.Profiling.Memory.Experimental;


public class UiManager : MonoBehaviour
{
    // Menus
    [HideInInspector] public bool inMenu = false;
    [HideInInspector] public bool inOverviewMenu = false;


    // In scene pull stuff
    public GameObject menues;
    
    public GameObject overviewMenu;
    
    public PlayerScript playerScript;
    
    public new Camera camera;
    
    [HideInInspector] public SaveJson saveJson;


    // Vars for items

    //Set in inspector first
    public ItemData[] itemsData;

    public ItemScript[] items;

    [HideInInspector] public int[] hotbarNums;

    [HideInInspector] public ItemSlotScript[] itemSlots;

    [HideInInspector] public GameObject itemPickedUp;

    [HideInInspector] public GameObject selectedItem;

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

            // Debug.Log(string.ioin(", ", items[x]));
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



    public void AddItem(GameObject itemFromGround)
    {
        foreach (ItemSlotScript i in itemSlots)
        {
            if (i.GetComponentInChildren<ItemScript>() == null)
            {
                itemFromGround.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                itemFromGround.transform.SetParent(i.transform);
                itemFromGround.transform.localRotation = new Quaternion(0, 0, 0, 0);

                break;
            }
        }
    }


    public void AddStackableItem(GameObject itemFromGround)
    {
        bool didStack = false;
        foreach (ItemScript i in items)
        {

            if (i is ItemScript && itemFromGround.name == i.gameObject.name)
            {

                Destroy(itemFromGround);
                i.itemData.count += itemFromGround.GetComponent<ItemScript>().itemData.count;
                didStack = true;
                break;
            }
        }
        if (didStack == false)
        {
            AddItem(itemFromGround);
        }
    }


    public void PickUpItem(GameObject itemFromGround)
    {
        if (itemFromGround.GetComponent<ItemScript>().itemData.itemType == ItemData.ItemType.STACKABLE)
        {

            AddStackableItem(itemFromGround);

        }
        else
        {

            AddItem(itemFromGround);
        
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
            
        }
            DeselectAll();
            GetItems();
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


    private void Start()
    {

        saveJson = GetComponent<SaveJson>();

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
    }
}
