using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    // TODO: Refactor a lot of each menus stuff onto a script on that menu

    public static UiManager instance { get; private set; }
    
    #region Vars
    // In scene pull stuff
    
    [SerializeField] new Camera camera;


    // Stats
    [SerializeField] Text maxHealthNum;
    [SerializeField] GameObject healthBar;
    [SerializeField] Image healthBarCurrent;
    [SerializeField] Text goldText;


    // Menu stuff
    bool itemCanPlace = false;
    [HideInInspector] public bool canChangePrice = false;
    [HideInInspector] public bool inMenu = false;
    // Set in inspector first
    public GameObject menu;
    public GameObject overviewMenu;
    public GameObject itemContainerPrompt;
    public GameObject settingsMenu;

    // Vendor Stuff
    public GameObject vendorMenu;
    public GameObject vendorPrompt;
    [SerializeField] GameObject vendorItemContainer;
    [SerializeField] GameObject vendorItem;
    [SerializeField] GameObject vendorMenuGoldAmount;
    [SerializeField] GameObject vendorName;
    [SerializeField] GameObject vendorText;
    [SerializeField] GameObject vendorPortrait;


    // Vars for items
    // Set in inspector first
    public ItemData[] itemsData;

    public ItemScript[] items;

    public ItemSlotScript[] itemSlots;

    [HideInInspector] public GameObject itemPickedUp;

    public GameObject selectedItem;

    [HideInInspector] public int lastItemSlotNum;

    #endregion  


    #region Items
    // Gets a list of all items
    public void GetItems()
    {
        Debug.Log("get items");

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
                itemToAdd.transform.SetParent(i.transform);
                itemToAdd.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                itemToAdd.transform.localRotation = new Quaternion(0, 0, 0, 0);
                itemToAdd.GetComponent<ItemScript>().itemData = Instantiate(itemToAdd.GetComponent<ItemScript>().itemData);

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

            if (i is ItemScript && itemToAdd.GetComponent<ItemScript>().itemData.itemName == i.itemData.itemName)
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

            selectedItem.transform.position = PlayerScript.instance.gameObject.transform.position;
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
            selectedItem.transform.SetParent(PlayerScript.instance.containerHit.transform.parent);
            selectedItem.transform.position = PlayerScript.instance.containerHit.transform.parent.position;
            selectedItem.transform.rotation = PlayerScript.instance.containerHit.transform.parent.rotation;
            selectedItem.transform.localScale = new Vector3(5, 5, 5);
            selectedItem.GetComponent<SphereCollider>().enabled = false;
            PlayerScript.instance.containerHit.transform.parent.transform.parent.GetComponent<PedestalScript>().itemOnSelfScript = selectedItem.GetComponent<ItemScript>();
            PlayerScript.instance.containerHit.transform.parent.transform.parent.GetComponent<PedestalScript>().ItemPlaced();
        }
        else
        {
            PlayerScript.instance.containerHit.transform.parent.transform.parent.GetComponent<PedestalScript>().ItemRemoved();
        }
    }


    // For item placing; does proper item placing depending upon if theres an item in the container or not
    public void ItemInteract()
    {
        ItemScript itemInContainer = PlayerScript.instance.containerHit.transform.parent.GetComponentInChildren<ItemScript>();
        if (itemInContainer != null)
        {
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
    public void MenuOpen(GameObject keepMenu)
    {

        // All menus close
        overviewMenu.SetActive(false);
        itemContainerPrompt.SetActive(false);
        settingsMenu.SetActive(false);
        vendorMenu.SetActive(false);

        // Controls the in menu variable and freeing the player when leaving a menu
        if (keepMenu != null)
        {

            inMenu = true;
            keepMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            menu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {

            inMenu = false;
            menu.SetActive(false);
            PlayerScript.instance.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;

        }
        if (! vendorMenu.activeSelf && vendorItemContainer.transform.childCount > 0)
        {          
            foreach (VendorItemScript child in vendorItemContainer.GetComponentsInChildren<VendorItemScript>())
            {
                Destroy(child.gameObject);
                Debug.Log("delete");
            }
            
        }
    }


    // Manages Vendor Menu
    public void VendorMenu()
    
    {

        VendorData currentVendorData = PlayerScript.instance.vendorHit.transform.gameObject.GetComponent<VendorScript>().vendorData;
        MenuOpen(vendorMenu);
        vendorMenuGoldAmount.GetComponent<Text>().text = PlayerScript.instance.playerData.gold.ToString();
        vendorText.GetComponent<Text>().text = currentVendorData.text;
        vendorName.GetComponent<Text>().text = currentVendorData.vendorName;
        vendorPortrait.GetComponent<Image>().sprite =  Resources.Load<Sprite>("VendorPortraits/" + currentVendorData.vendorName);
        for (int i = 0; i < currentVendorData.unlockedWares; i++)
        {       
            GameObject newItemToSell = Instantiate(vendorItem, vendorItemContainer.transform);
            newItemToSell.GetComponent<VendorItemScript>().itemData = currentVendorData.wares[i];
            newItemToSell.GetComponentInChildren<Text>().text = "G " + currentVendorData.wares[i].price.ToString();
            newItemToSell.transform.Find("ItemContainer").transform.Find("ItemToSell").GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImages/" + currentVendorData.wares[i].itemName);
        }
        

    }

    // TODO: Move this to player look script like vendor prompt
    // Manages ItemContainerPrompt
    public void ItemContainerPrompt(bool canPlaceItem)
    {
        if (canPlaceItem)
        {

            itemContainerPrompt.SetActive(true);

        }
        else if (itemContainerPrompt.activeSelf && ! canPlaceItem)
        {

            itemContainerPrompt.SetActive(false);

        }
        itemCanPlace = canPlaceItem;
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
    // Button param is soley for when you press the settings button in the overview menu
    public void SettingsMenu()
    {

        MenuOpen(settingsMenu);

    }


    public void QuitToDesktop()
    {
        SaveJson.instance.Save();
        Application.Quit();
    }


    public void QuitToMainMenu()
    {
        Debug.Log("Save Json: " + SaveJson.instance);
        SaveJson.instance.Save();
        SceneManager.LoadScene("Start");
    }


    // Checks if the player is in a menu, if so, the player can no longer do anything, locks mouse, opens menus
    void CheckMenu()
    {
        OverviewMenu();


        if (inMenu && Input.GetKeyDown("escape"))
        {
            MenuOpen(null);
        }
        else if (!inMenu && Input.GetKeyDown("escape"))
        {
            SettingsMenu();
        }
    }

    #endregion


    #region Stats


    public void UpdateStats()
    {
        // Stats Overview
        maxHealthNum.text = PlayerScript.instance.playerData.maxHealth.ToString();

        // Health bar
        healthBar.transform.localScale = new Vector3(1 * (1 + (int.Parse(maxHealthNum.text)-100) * 0.0025f), 1, 1);
        healthBarCurrent.fillAmount = PlayerScript.instance.playerData.currentHealth / PlayerScript.instance.playerData.maxHealth;

        // Gold
        goldText.text = PlayerScript.instance.playerData.gold.ToString();
        vendorMenuGoldAmount.GetComponent<Text>().text = PlayerScript.instance.playerData.gold.ToString();
    }

    #endregion


    #region Function Calls

    void OnEnable()
    {
        PlayerScript.playerStatChanged += UpdateStats;
        GameControllerScript.itemSale += UpdateStats;
    }

    void OnDisable()
    {
        PlayerScript.playerStatChanged -= UpdateStats;
        GameControllerScript.itemSale -= UpdateStats;
    }

    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
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
