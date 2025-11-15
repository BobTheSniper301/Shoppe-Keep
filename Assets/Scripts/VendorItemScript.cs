using UnityEngine.UI;
using UnityEngine;


public class VendorItemScript : MonoBehaviour
{
    public ItemData itemData;

    public void PurchaseItem()
    {
        PlayerScript.instance.playerData.gold -= itemData.price;
        GameObject itemToAdd = Instantiate(SaveJson.instance.itemPrefab, PlayerScript.instance.transform.position, Quaternion.identity);
        itemToAdd.GetComponent<ItemScript>().itemData = Instantiate(itemData);
        itemToAdd.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImages/" + itemData.itemName);
        itemToAdd.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ItemImages/" + itemData.itemName);
        itemToAdd.name = itemData.itemName;
        UiManager.instance.PickUpItem(itemToAdd);
        GameControllerScript.itemSale?.Invoke();
    }
}
