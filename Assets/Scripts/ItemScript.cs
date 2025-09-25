using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Image image;

    //[HideInInspector] public Transform parentBeforeDrag;

    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}