using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler
{

  

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            ItemScript item = eventData.pointerDrag.GetComponent<ItemScript>();
            item.parentAfterDrag = transform;
        }
        else if (transform.childCount == 1)
        {
            ItemScript slottedItem = gameObject.GetComponentInChildren<ItemScript>();
            ItemScript item = eventData.pointerDrag.GetComponent<ItemScript>();
            slottedItem.transform.SetParent(item.parentAfterDrag);
            item.parentAfterDrag = transform;
            item.OnEndDrag(eventData);
        }
    }
}
