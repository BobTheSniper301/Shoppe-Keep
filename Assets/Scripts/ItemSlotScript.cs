using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This scripts runs on the Inventory slot that the item is being moved to
public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    public int slotNum;

    [HideInInspector]  public bool selected;

    [HideInInspector] public ItemScript item;
    [HideInInspector] public ItemScript slottedItem;
    public UIManager uiManager;

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

    private void Start()
    {
        uiManager = GetComponentInParent<UIManager>();
        GetComponent<Image>();
        ColorUtility.TryParseHtmlString("#84FFDF", out selectionColor);
    }

}
