using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This scripts runs on the Inventory slot that the item is being moved to
public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    // Object grab stuff
    public UIManager uiManager;

    //[HideInInspector] ScriptableObject scriptItemInSlot;


    // Random ones
    public int slotNum;

    public bool selected;


    // Item stuff
    [HideInInspector] public ItemScript item;
    [HideInInspector] public ItemScript slottedItem;
    

    // Color vars
    Color selectionColor;
    public Image slotImage;


    public void OnDrop(PointerEventData eventData)
    { 
        ItemScript item = eventData.pointerDrag.GetComponent<ItemScript>();
        ItemScript slottedItem = gameObject.GetComponentInChildren<ItemScript>();

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

    public void Selected()
    {
        Debug.Log("Selected");
        selected = true;
        ItemScript childItem = GetComponentInChildren<ItemScript>();
        ItemData scriptItemInSlot = childItem.GetComponent<ItemScript>().itemData;
        scriptItemInSlot.itemToDisplay.SetActive(true);

        ColorUtility.TryParseHtmlString("#84FFDF", out selectionColor);
        GetComponent<Image>().color = selectionColor;

    }


    private void Start()
    {
        uiManager = GetComponentInParent<UIManager>();
    }
}
