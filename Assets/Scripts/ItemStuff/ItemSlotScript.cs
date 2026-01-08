using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This scripts runs on the Inventory slot that the item is being moved to
public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    // Item stuff
    [HideInInspector] public ItemScript item;
    [HideInInspector] public ItemScript slottedItem;
    [SerializeField] ItemManagerScript itemManagerScript;

    // Color vars
    Color selectionColor;


    // ItemSlot stuff
    //    public int slotNum;

    [HideInInspector] public bool lastSelected;

    [HideInInspector] public bool toggled;


    // Runs when the dragged item is dropped into this slot
    public void OnDrop(PointerEventData eventData)
    {
        slottedItem = this.GetComponentInChildren<ItemScript>();
        // Gets the item and updates slotted item
        item = eventData.pointerDrag.GetComponent<ItemScript>();
        // UpdateItemSlot();

        // if no other item, just move the dropped item into this slot
        if (transform.childCount == 1)
        {
            //GetComponentInChildren<Text>().text = item.count.ToString();
            item.parentBeforeDrag = transform;
        }
        // if there is an item, swap where they are and the item stacks text
        else if (transform.childCount == 2)
        {
            // Update stackable item amount
                //int slottedItemAmount = slottedItem.count;
                //int itemAmount = item.count;
                //item.parentBeforeDrag.GetComponentInChildren<Text>().text = slottedItemAmount.ToString();
                //slottedItem.transform.parent.GetComponentInChildren<Text>().text = itemAmount.ToString();
            // Reparents
            slottedItem.transform.SetParent(item.parentBeforeDrag);
            item.parentBeforeDrag = transform;
        }
        item.OnEndDrag(eventData);

        // Updates the Items list with the new parents
        //uiManager.GetItems();
    }


    //    // Updates the slotted item, and shows proper text if there is a stackable item or not
    //    public void UpdateItemSlot()
    //    {
    //        slottedItem = gameObject.GetComponentInChildren<ItemScript>();
    //        if (slottedItem && slottedItem.itemData.itemType == ItemData.ItemType.STACKABLE)
    //            gameObject.GetComponentInChildren<Text>().text = slottedItem.itemData.count.ToString();
    //        else if ((!slottedItem || slottedItem.itemData.itemType != ItemData.ItemType.STACKABLE))
    //            gameObject.GetComponentInChildren<Text>().text = "";
    //    }


    //    // Checks each item in the heldObjects list, finds what matches the slotted item, then shows it the held object
    //    void ShowItem()
    //    {
    //        Debug.Log("showitem");
    //        foreach (Transform i in gameController.heldObjects)
    //        {
    //            if (i.name == slottedItem.GetComponent<ItemScript>().itemData.itemName)
    //            {
    //                i.gameObject.SetActive(true);
    //            }
    //        }
    //    }

    //    // Inverse of ShowItem()
    //    void HideItem()
    //    {
    //        foreach (Transform i in gameController.heldObjects)
    //        {
    //            if (i.name == slottedItem.GetComponent<ItemScript>().itemData.itemName)
    //            {
    //                i.gameObject.SetActive(false);
    //            }
    //        }
    //    }


    //    // Called when player presses the key corresponding to this slot
    //    // Change color, show item, update selected item
    public void Select()
    {
        itemManagerScript.selectedItemSlotScript = this;
        // Shows the item slotted if there is one
        //if (slottedItem)
        //{
        //    ShowItem();
        //}

        // Updates the current selected item
        if (transform.childCount > 1)
        {
            itemManagerScript.selectedItemScript = slottedItem;
        }
        else
        {
            itemManagerScript.selectedItemScript = null;
        }

        // Changes color when selected
        ColorUtility.TryParseHtmlString("#84FFDF", out selectionColor);
        GetComponent<Image>().color = selectionColor;

    }

    // Inverse of Select()
    public void Deselect()
    {
        //if (slottedItem)
        //{
        //    HideItem();
        //}

        GetComponent<Image>().color = Color.white;
    }

    //    void Awake()
    //    {

    //        uiManager = GetComponentInParent<UiManager>();

    //    }
}

////    // Object grab stuff
////    public UiManager uiManager;
////    public GameControllerScript gameController;


////    // Item stuff
////    [HideInInspector] public ItemScript item;
////    [HideInInspector] public ItemScript slottedItem;


////    // Color vars
////    Color selectionColor;
////    public Image slotImage;


////    // ItemSlot stuff
////    public int slotNum;

////    [HideInInspector] public bool lastSelected;

////    [HideInInspector] public bool toggled;


////    // Runs when the dragged item is dropped into this slot
////    public void OnDrop(PointerEventData eventData)
////    {
////        // Gets the item and updates slotted item
////        item = eventData.pointerDrag.GetComponent<ItemScript>();
////        // UpdateItemSlot();

////        // if no other item, just move the dropped item into this slot
////        if (transform.childCount == 1)
////        {
////            GetComponentInChildren<Text>().text = item.itemData.count.ToString();
////            item.parentBeforeDrag = transform;
////        }
////        // if there is an item, swap where they are and the item stacks text
////        else if (transform.childCount == 2)
////        {
////            // Update stackable item amount
////            int slottedItemAmount = slottedItem.itemData.count;
////            int itemAmount = item.itemData.count;
////            item.parentBeforeDrag.GetComponentInChildren<Text>().text = slottedItemAmount.ToString();
////            slottedItem.transform.parent.GetComponentInChildren<Text>().text = itemAmount.ToString();
////            // Reparents
////            slottedItem.transform.SetParent(item.parentBeforeDrag);
////            item.parentBeforeDrag = transform;
////        }
////        item.OnEndDrag(eventData);

////        // Updates the Items list with the new parents
////        uiManager.GetItems();
////    }


////    // Updates the slotted item, and shows proper text if there is a stackable item or not
////    public void UpdateItemSlot()
////    {
////        slottedItem = gameObject.GetComponentInChildren<ItemScript>();
////        if (slottedItem && slottedItem.itemData.itemType == ItemData.ItemType.STACKABLE)
////            gameObject.GetComponentInChildren<Text>().text = slottedItem.itemData.count.ToString();
////        else if ((!slottedItem || slottedItem.itemData.itemType != ItemData.ItemType.STACKABLE))
////            gameObject.GetComponentInChildren<Text>().text = "";
////    }


////    // Checks each item in the heldObjects list, finds what matches the slotted item, then shows it the held object
////    void ShowItem()
////    {
////        Debug.Log("showitem");
////        foreach (Transform i in gameController.heldObjects)
////        {
////            if (i.name == slottedItem.GetComponent<ItemScript>().itemData.itemName)
////            {
////                i.gameObject.SetActive(true);
////            }
////        }
////    }

////    // Inverse of ShowItem()
////    void HideItem()
////    {
////        foreach (Transform i in gameController.heldObjects)
////        {
////            if (i.name == slottedItem.GetComponent<ItemScript>().itemData.itemName)
////            {
////                i.gameObject.SetActive(false);
////            }
////        }
////    }


////    // Called when player presses the key corresponding to this slot
////    // Change color, show item, update selected item
////    public void Selected()
////    {
////        // Shows the item slotted if there is one
////        if (slottedItem)
////        {
////            ShowItem();
////        }

////        // Updates the current selected item
////        if (transform.childCount > 1)
////        {
////            uiManager.selectedItem = GetComponentInChildren<ItemScript>().gameObject;
////        }
////        else
////        {
////            uiManager.selectedItem = null;
////        }

////        lastSelected = true;

////        // Changes color when selected
////        ColorUtility.TryParseHtmlString("#84FFDF", out selectionColor);
////        GetComponent<Image>().color = selectionColor;

////    }

////    // Inverse of Selected()
////    public void Deselected()
////    {
////        if (slottedItem)
////        {
////            HideItem();
////        }

////        lastSelected = false;

////        GetComponent<Image>().color = Color.white;
////    }

////    // Can deselect the already selected item slot
////    public void ToggleSelect()
////    {
////        if (lastSelected)
////        {
////            Deselected();
////            lastSelected = false;
////            toggled = true;
////        }
////        else
////        {
////            toggled = false;
////        }

////    }

////    void Awake()
////    {

////        uiManager = GetComponentInParent<UiManager>();

////    }