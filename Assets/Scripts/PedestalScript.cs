using UnityEngine;
using UnityEngine.UI;

public class PedestalScript : MonoBehaviour
{

    float itemPrice;
    public ItemScript placedItemScript;
    [SerializeField] Text itemPriceText;
    [SerializeField] GameObject priceInterface;
    [SerializeField] GameObject itemContainer;

    public void Interact()
    {
        if (ItemManager.instance.selectedItemScript)
        {
            if (placedItemScript)
            {
                SwapItem();
            }
            else
            {
                PlaceItem();
            }
        }
        else if (placedItemScript)
        {
            RemoveItem();
        }
        PedestalChange();
        ItemManager.instance.GetItems();
    }

    public void LowerPrice()
    {
        itemPrice -= PlayerScript.instance.priceChangePower;
        placedItemScript.itemData.price -= PlayerScript.instance.priceChangePower;
        PedestalChange();
    }

    public void IncreasePrice()
    {
        itemPrice += PlayerScript.instance.priceChangePower;
        placedItemScript.itemData.price += PlayerScript.instance.priceChangePower;
        PedestalChange();
    }

    void PedestalChange()
    {
        // Stops negative prices
        itemPrice = Mathf.Clamp(itemPrice, 0f, 1000000);

        itemPriceText.text = itemPrice.ToString();
    }

    void PlaceItem()
    {
        ItemScript selectedItem = ItemManager.instance.selectedItemScript;
        
        placedItemScript = selectedItem;
        
        selectedItem.transform.parent.GetComponent<ItemSlotScript>().Deselect();

        // Moves the physical item
        selectedItem.transform.SetParent(itemContainer.transform, false);
        selectedItem.transform.position = itemContainer.transform.position;
        
        itemPriceText.transform.parent.transform.parent.gameObject.SetActive(true);
        itemPrice = placedItemScript.itemData.price;
        
        //GameControllerScript.instance.pedestals.Add(this.transform.Find("NPCSpot").gameObject);
    }

    void RemoveItem()
    {
        ItemManager.instance.AddItem(placedItemScript.gameObject);

        placedItemScript = null;
        priceInterface.SetActive(false);

        //GameControllerScript.instance.pedestals.Remove(this.transform.Find("NPCSpot").gameObject);
    }

    void SwapItem()
    {
        ItemScript selectedItem = ItemManager.instance.selectedItemScript;
        ItemScript alreadyPlacedItemScript = placedItemScript;
        // Handle new item
        placedItemScript = selectedItem;
        
        selectedItem.transform.parent.GetComponent<ItemSlotScript>().Deselect();

        // Moves the physical item
        selectedItem.transform.SetParent(itemContainer.transform, false);
        selectedItem.transform.position = itemContainer.transform.position;
        
        itemPriceText.transform.parent.transform.parent.gameObject.SetActive(true);
        itemPrice = placedItemScript.itemData.price;


        // Handle old item
        ItemManager.instance.AddItem(alreadyPlacedItemScript.gameObject);
    }


    void Start()
    {
        priceInterface.SetActive(false);
    }
}
