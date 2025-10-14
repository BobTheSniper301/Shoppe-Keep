using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public ItemData itemData;

    public Image image;

    GameObject player;

    UiManager uiManager;

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


    void OnTriggerStay(Collider other)
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
        Debug.Log("MOVE");
    }


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
    }

    void Update()
    {
        // Reaches the player
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.2f)
        {
            uiManager.PickUpItem(this.gameObject);
        }
    }

}