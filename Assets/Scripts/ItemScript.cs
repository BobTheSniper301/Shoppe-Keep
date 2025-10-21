using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public ItemData itemData;

    [HideInInspector] public Image image;

    GameObject player;

    UiManager uiManager;

    [HideInInspector] public Transform parentBeforeDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentBeforeDrag);
        image.raycastTarget = true;
    }


    void OnTriggerStay(Collider other)
    {
        // atempt to set up as an async function
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
        // Reaches the player
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.2f)
        {
            // Debug.Log("pickup");
            uiManager.PickUpItem(this.gameObject);
        }
    }


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
    }
}