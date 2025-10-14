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
        updateSlottedItem();

        if (transform.childCount == 0)
        {
            item.parentAfterDrag = transform;
        }
        else if (transform.childCount == 1)
        {
            // Reparents
            slottedItem.transform.SetParent(item.parentAfterDrag);
            item.parentAfterDrag = transform;
        }
        item.OnEndDrag(eventData);
        // Updates the Items list with the new parents
        uiManager.getItems();
    }

    public void updateSlottedItem()
    {
        slottedItem = gameObject.GetComponentInChildren<ItemScript>();
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

        uiManager.selectedItem = GetComponentInChildren<ItemScript>().gameObject;

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


    private void Start()
    {
        uiManager = GetComponentInParent<UiManager>();
    }
}
