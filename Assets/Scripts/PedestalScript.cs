// using UnityEngine;
// using UnityEngine.UI;

// public class PedestalScript : MonoBehaviour
// {

//     public float itemPrice;
//     public ItemScript itemOnSelfScript;
//     [SerializeField] Text itemPriceText;


//     public void PedestalChange(string interactedButton, int value)
//     {
        

//         if (interactedButton != null)
//         {
//             if (interactedButton == "Minus")
//             {
                
//                 itemPrice -= value;
//             }
//             else if (interactedButton == "Plus")
//             {
                
//                 itemPrice += value;
//             }
//             itemOnSelfScript.itemData.price = itemPrice;
//         }
//         itemPrice = Mathf.Clamp(itemPrice, 0f, 1000000);

//         itemPriceText.text = itemPrice.ToString();

//     }

//     public void ItemPlaced()
//     {
//         itemPrice = itemOnSelfScript.itemData.price;
//         itemPriceText.transform.parent.transform.parent.gameObject.SetActive(true);
//         GameControllerScript.instance.pedestals.Add(this.transform.Find("NPCSpot").gameObject);
//         PedestalChange(null, 0);
//     }

//     public void ItemRemoved()
//     {
//         itemOnSelfScript = null;
//         GameControllerScript.instance.pedestals.Remove(this.transform.Find("NPCSpot").gameObject);
//         itemPriceText.transform.parent.transform.parent.gameObject.SetActive(false);
//     }


//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         GameObject priceTextOnSelf = transform.Find("PriceInterface").transform.Find("Price").transform.Find("PriceText").gameObject;
//         itemPriceText = priceTextOnSelf.GetComponent<Text>();
//         itemPriceText.transform.parent.transform.parent.gameObject.SetActive(false);
        
//     }
// }
