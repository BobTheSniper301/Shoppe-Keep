using UnityEngine;
using UnityEngine.EventSystems;

// This scripts runs on the Inventory slot that the item is being moved to
public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    public int slotNum;

    public ItemScript item;
    public ItemScript slottedItem;

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


            item.OnEndDrag(eventData);
        }
        
    }

    private void Start()
    {
        
    }

}
