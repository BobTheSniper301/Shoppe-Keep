using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This scripts runs on the Inventory slot that the item is being moved to
public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    // Object grab stuff
    public UiManager uiManager;
    public GameControllerScript gameController;


    // Random ones



    // Item stuff
    [HideInInspector] public ItemScript item;
    [HideInInspector] public ItemScript slottedItem;



    // Color vars
    Color selectionColor;
    public Image slotImage;


    // Self stuff
    public int slotNum;

    [HideInInspector] public bool lastSelected;

    [HideInInspector] public bool toggled;


    public void OnDrop(PointerEventData eventData)
    {
        item = eventData.pointerDrag.GetComponent<ItemScript>();
        UpdateItemSlot();

        if (transform.childCount == 1)
        {
            GetComponentInChildren<Text>().text = item.itemData.count.ToString();
            item.parentBeforeDrag = transform;
        }
        else if (transform.childCount == 2)
        {
            // Update stackable item amount
            int slottedItemAmount = slottedItem.itemData.count;
            int itemAmount = item.itemData.count;
            item.parentBeforeDrag.GetComponentInChildren<Text>().text = slottedItemAmount.ToString();
            slottedItem.transform.parent.GetComponentInChildren<Text>().text = itemAmount.ToString();
            // Reparents
            slottedItem.transform.SetParent(item.parentBeforeDrag);
            item.parentBeforeDrag = transform;
        }
        item.OnEndDrag(eventData);
        // Updates the Items list with the new parents
        uiManager.GetItems();
    }


    public void UpdateItemSlot()
    {
        slottedItem = gameObject.GetComponentInChildren<ItemScript>();
        if (slottedItem && slottedItem.itemData.itemType == ItemData.ItemType.STACKABLE)
            gameObject.GetComponentInChildren<Text>().text = slottedItem.itemData.count.ToString();
        else if ((!slottedItem || slottedItem.itemData.itemType != ItemData.ItemType.STACKABLE))
           gameObject.GetComponentInChildren<Text>().text = "";
    }


    void ShowItem()
    {
        foreach (Transform i in gameController.heldObjects)
        {
            if (i.name == slottedItem.name)
            {
                i.gameObject.SetActive(true);
            }
        }
    }


    void HideItem()
    {
        foreach (Transform i in gameController.heldObjects)
        {
            if (i.name == slottedItem.name)
            {
                i.gameObject.SetActive(false);
            }
        }
    }


    public void Selected()
    {
        if (slottedItem)
        {
            ShowItem();
        }

        if (transform.childCount > 1)
        {
            uiManager.selectedItem = GetComponentInChildren<ItemScript>().gameObject;
        }
        else
        {
            uiManager.selectedItem = null;
        }

        lastSelected = true;

        ColorUtility.TryParseHtmlString("#84FFDF", out selectionColor);
        GetComponent<Image>().color = selectionColor;

    }


    public void Deselected()
    {
        if (slottedItem)
        {
            HideItem();
        }

        lastSelected = false;

        GetComponent<Image>().color = Color.white;
    }


    public void ToggleSelect()
    {
        if (lastSelected)
        {
            Deselected();
            lastSelected = false;
            toggled = true;
        }
        else
        {
            toggled = false;
        }
        
    }

    void Awake()
    {

        uiManager = GetComponentInParent<UiManager>();
        
    }
}
