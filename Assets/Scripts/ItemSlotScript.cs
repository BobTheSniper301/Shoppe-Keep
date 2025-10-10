using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This scripts runs on the Inventory slot that the item is being moved to
public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    // Object grab stuff
    public UIManager uiManager;
    public GameControllerScript gameController;

    //[HideInInspector] ScriptableObject scriptItemInSlot;


    // Random ones



    // Item stuff
    [HideInInspector] public ItemScript item;
    [HideInInspector] public ItemScript slottedItem;



    // Color vars
    Color selectionColor;
    public Image slotImage;


    // Self stuff
    public int slotNum;

    public bool selected;



    public void OnDrop(PointerEventData eventData)
    { 
        ItemScript item = eventData.pointerDrag.GetComponent<ItemScript>();
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

    void updateSlottedItem()
    {
        ItemScript slottedItem = gameObject.GetComponentInChildren<ItemScript>();
    }


    public void Selected()
    {
        Debug.Log("Selected");

        foreach (Transform i in gameController.heldObjects)
        {
            Debug.Log("hi"); 
            Debug.Log("i name " + i.name);
            Debug.Log("slotitem name " + slottedItem.name);
            if (i.name == slottedItem.name)
            {
                Debug.Log("name is true");
                i.gameObject.SetActive(true);
            }
        }

        selected = true;
        

        ColorUtility.TryParseHtmlString("#84FFDF", out selectionColor);
        GetComponent<Image>().color = selectionColor;

    }


    private void Start()
    {
        uiManager = GetComponentInParent<UIManager>();
        updateSlottedItem();
    }
}
