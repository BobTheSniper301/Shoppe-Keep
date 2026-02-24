using UnityEngine.UI;
using UnityEngine;


public class VendorItemScript : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] GameObject itemImageContainer;

    public void PurchaseItem()
    {
        PlayerScript.instance.gold -= itemData.price;
        GameObject itemToAdd = Instantiate(GameControllerScript.instance.itemPrefab, PlayerScript.instance.transform.position, Quaternion.identity);
        itemToAdd.GetComponent<ItemScript>().itemData = Instantiate(itemData);
        itemToAdd.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImages/" + itemData.itemName);
        itemToAdd.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ItemImages/" + itemData.itemName);
        itemToAdd.name = itemData.itemName;
        ItemManager.instance.AddItem(itemToAdd);
        EventManager.itemSale?.Invoke();
    }

    itemImageContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImages/" + itemData.itemName);
}
