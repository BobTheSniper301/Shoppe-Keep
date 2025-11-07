using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PedestalScript : MonoBehaviour
{

    public float itemPrice;
    public ItemScript itemOnSelfScript;
    [SerializeField] Text itemPriceText;


    public void PedestalChange(string interactedButton, int value)
    {
        

        if (interactedButton != null)
        {
            if (interactedButton == "PurchaseButton" && itemOnSelfScript != null)
            {
                PlayerScript.instance.playerData.gold -= itemPrice;
                Destroy(itemOnSelfScript.gameObject);
                ItemRemoved();
                return;
            }
            else if (interactedButton == "Minus")
            {
                Debug.Log("minus");
                itemPrice -= value;
            }
            else if (interactedButton == "Plus")
            {
                Debug.Log("plus");
                itemPrice += value;
            }
            itemOnSelfScript.itemData.price = itemPrice;
        }
        itemPrice = Mathf.Clamp(itemPrice, 0f, 1000000);

        itemPriceText.text = itemPrice.ToString();

    }

    public void ItemPlaced()
    {
        itemPrice = itemOnSelfScript.itemData.price;
        itemPriceText.transform.parent.transform.parent.gameObject.SetActive(true);
        PedestalChange(null, 0);
    }

    public void ItemRemoved()
    {
        itemOnSelfScript = null;
        itemPriceText.transform.parent.transform.parent.gameObject.SetActive(false);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameControllerScript.instance.pedestals.Add(this.transform.Find("NPCSpot").gameObject);

        GameObject priceTextOnSelf = transform.Find("PriceInterface").transform.Find("Price").transform.Find("PriceText").gameObject;
        itemPriceText = priceTextOnSelf.GetComponent<Text>();
        itemPriceText.transform.parent.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
