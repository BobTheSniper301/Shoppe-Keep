using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PedestalScript : MonoBehaviour
{

    public int itemPrice;
    public ItemScript itemOnSelfScript;
    [SerializeField] Text itemPriceText;


    public void PedestalChange(string interactedButton, int value)
    {
        

        if (interactedButton != null)
        {
            if (interactedButton == "Minus")
            {
                Debug.Log("minus");
                itemPrice -= value;
            }
            if (interactedButton == "Plus")
            {
                Debug.Log("plus");
                itemPrice += value;
            }
            itemOnSelfScript.itemData.price = itemPrice;
        }

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
        GameObject priceTextOnSelf = transform.Find("PriceInterface").transform.Find("Price").transform.Find("PriceText").gameObject;
        itemPriceText = priceTextOnSelf.GetComponent<Text>();
        itemPriceText.transform.parent.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
