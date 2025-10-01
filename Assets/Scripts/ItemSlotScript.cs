using UnityEngine;
using UnityEngine.EventSystems;

// This scripts runs on the Inventory slot that the item is being moved to
public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    public int slotNum;
  

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            ItemScript item = eventData.pointerDrag.GetComponent<ItemScript>();
            item.parentAfterDrag = transform;
            item.itemData.itemNum = slotNum;
            //Debug.Log(slotNum);
        }
        else if (transform.childCount == 1)
        {
            ItemScript slottedItem = gameObject.GetComponentInChildren<ItemScript>();
            ItemScript item = eventData.pointerDrag.GetComponent<ItemScript>();
            // updates the items slot number
            slottedItem.itemData.itemNum = item.itemData.itemNum;
            item.itemData.itemNum = slotNum;
            
            // Reparents
            slottedItem.transform.SetParent(item.parentAfterDrag);
            item.parentAfterDrag = transform;

            //Debug.Log(slotNum);

            item.OnEndDrag(eventData);
        }
        
    }
}
