using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public ItemData itemData;

    public int count = 1;

    public Image image;

    //    GameObject player;

    //    UiManager uiManager;

    [HideInInspector] public Transform parentBeforeDrag;


    // Runs when item starts getting dragged
    // Remove raycast forself-for drop, and change parent
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;

        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    // Runs when items is being dragged
    // Item follows mouse
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }


    // Runs when the item is dropped
    // Reparents, and makes itself a raycast target 
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentBeforeDrag);
        image.raycastTarget = true;
    }

    //    // Runs when the player is in the collider
    //    // Moves toward the player, when close enough it gets picked up (uiManager.PickUpItem())
    //    void OnTriggerStay(Collider other)
    //    {
    //        // TODO: atempt to set up as an async function
    //        if (other.gameObject.name != "Player")
    //        {
    //            return;
    //        }

    //        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
    //        // Reaches the player
    //        if (Vector3.Distance(transform.position, player.transform.position) <= 1.2f)
    //        {
    //            uiManager.PickUpItem(this.gameObject);
    //        }
    //    }


    //    void Start()
    //    {

    //        player = GameObject.FindWithTag("Player");
    //        uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();

    //    }
    //}

    ////public ItemData itemData;

    ////     [HideInInspector] public Image image;

    ////     GameObject player;

    ////     UiManager uiManager;

    ////     [HideInInspector] public Transform parentBeforeDrag;


    ////     // Runs when item starts getting dragged
    ////     // Remove raycast forself-for drop, and change parent
    ////     public void OnBeginDrag(PointerEventData eventData)
    ////     {
    ////         image.raycastTarget = false;

    ////         parentBeforeDrag = transform.parent;
    ////         transform.SetParent(transform.root);
    ////         transform.SetAsLastSibling();
    ////     }

    ////     // Runs when items is being dragged
    ////     // Item follows mouse
    ////     public void OnDrag(PointerEventData eventData)
    ////     {
    ////         transform.position = Input.mousePosition;
    ////     }


    ////     // Runs when the item is dropped
    ////     // Reparents, and makes itself a raycast target 
    ////     public void OnEndDrag(PointerEventData eventData)
    ////     {
    ////         transform.SetParent(parentBeforeDrag);
    ////         image.raycastTarget = true;
    ////     }

    ////     // Runs when the play is in the collider
    ////     // Moves toward the player, when close enough it gets picked up (uiManager.PickUpItem())
    ////     void OnTriggerStay(Collider other)
    ////     {
    ////         // TODO: atempt to set up as an async function
    ////         if (other.gameObject.name != "Player")
    ////         {
    ////             return;
    ////         }
    ////         transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
    ////         // Reaches the player
    ////         if (Vector3.Distance(transform.position, player.transform.position) <= 1.2f)
    ////         {
    ////             uiManager.PickUpItem(this.gameObject);
    ////         }
    ////     }


    ////     void Start()
    ////     {

    ////         player = GameObject.FindWithTag("Player");
    ////         uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();

}
//    public ItemData itemData;

//    [HideInInspector] public Image image;

//    GameObject player;

//    UiManager uiManager;

//    [HideInInspector] public Transform parentBeforeDrag;


//    // Runs when item starts getting dragged
//    // Remove raycast forself-for drop, and change parent
//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        image.raycastTarget = false;

//        parentBeforeDrag = transform.parent;
//        transform.SetParent(transform.root);
//        transform.SetAsLastSibling();
//    }

//    // Runs when items is being dragged
//    // Item follows mouse
//    public void OnDrag(PointerEventData eventData)
//    {
//        transform.position = Input.mousePosition;
//    }


//    // Runs when the item is dropped
//    // Reparents, and makes itself a raycast target 
//    public void OnEndDrag(PointerEventData eventData)
//    {
//        transform.SetParent(parentBeforeDrag);
//        image.raycastTarget = true;
//    }

//    // Runs when the play is in the collider
//    // Moves toward the player, when close enough it gets picked up (uiManager.PickUpItem())
//    void OnTriggerStay(Collider other)
//    {
//        // TODO: atempt to set up as an async function
//        if (other.gameObject.name != "Player")
//        {
//            return;
//        }
        
//        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
//        // Reaches the player
//        if (Vector3.Distance(transform.position, player.transform.position) <= 1.2f)
//        {
//            uiManager.PickUpItem(this.gameObject);
//        }
//    }


//    void Start()
//    {

//        player = GameObject.FindWithTag("Player");
//        uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();

//    }
//}

////public ItemData itemData;

////     [HideInInspector] public Image image;

////     GameObject player;

////     UiManager uiManager;

////     [HideInInspector] public Transform parentBeforeDrag;


////     // Runs when item starts getting dragged
////     // Remove raycast forself-for drop, and change parent
////     public void OnBeginDrag(PointerEventData eventData)
////     {
////         image.raycastTarget = false;

////         parentBeforeDrag = transform.parent;
////         transform.SetParent(transform.root);
////         transform.SetAsLastSibling();
////     }

////     // Runs when items is being dragged
////     // Item follows mouse
////     public void OnDrag(PointerEventData eventData)
////     {
////         transform.position = Input.mousePosition;
////     }


////     // Runs when the item is dropped
////     // Reparents, and makes itself a raycast target 
////     public void OnEndDrag(PointerEventData eventData)
////     {
////         transform.SetParent(parentBeforeDrag);
////         image.raycastTarget = true;
////     }

////     // Runs when the play is in the collider
////     // Moves toward the player, when close enough it gets picked up (uiManager.PickUpItem())
////     void OnTriggerStay(Collider other)
////     {
////         // TODO: atempt to set up as an async function
////         if (other.gameObject.name != "Player")
////         {
////             return;
////         }
////         transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
////         // Reaches the player
////         if (Vector3.Distance(transform.position, player.transform.position) <= 1.2f)
////         {
////             uiManager.PickUpItem(this.gameObject);
////         }
////     }


////     void Start()
////     {

////         player = GameObject.FindWithTag("Player");
////         uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
