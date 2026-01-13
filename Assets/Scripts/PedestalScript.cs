using UnityEngine;
using UnityEngine.UI;

public class PedestalScript : MonoBehaviour
{

    public float itemPrice;
    public ItemScript placedItemScript;
    [SerializeField] Text itemPriceText;
    [SerializeField] GameObject priceInterface;
    [SerializeField] GameObject itemContainer;
    [SerializeField] GameObject NPCSpot;

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
        placedItemScript.itemData.price -= PlayerScript.instance.priceChangePower;
        PedestalChange();
    }

    public void IncreasePrice()
    {
        placedItemScript.itemData.price += PlayerScript.instance.priceChangePower;
        PedestalChange();
    }

    void PedestalChange()
    {
        if (!placedItemScript)
        {
            itemPrice = 0;
            return;
        }
        
        itemPrice = placedItemScript.itemData.price;

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
        
        priceInterface.SetActive(true);

        PedestalChange();
        
        AiManager.instance.pedestals.Add(NPCSpot);

        EventManager.pedestalChanged?.Invoke();
    }

    public void RemoveItem()
    {
        ItemManager.instance.AddItem(placedItemScript.gameObject);

        
        placedItemScript = null;
        
        priceInterface.SetActive(false);

        PedestalChange();

        AiManager.instance.pedestals.Remove(NPCSpot);

        EventManager.pedestalChanged?.Invoke();
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

        PedestalChange();
    }

    
    public void PurchaseItem()
    {
        Debug.Log("purchase");
        
        // Similar to RemoveItem() but without giving the player an item back
        priceInterface.SetActive(false);

        PlayerScript.instance.gold += placedItemScript.itemData.price; 
        EventManager.itemSale?.Invoke();

        Destroy(placedItemScript.gameObject);

        placedItemScript = null;

        PedestalChange();

        AiManager.instance.pedestals.Remove(NPCSpot);

        EventManager.pedestalChanged?.Invoke();
    }
}
