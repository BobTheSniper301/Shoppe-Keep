using UnityEngine;

public class VendorScript : MonoBehaviour
{

    public VendorData vendorData;
    [SerializeField] VendorMenuScript vendorMenuScript;


    public void UpdateVendor()
    {
         Debug.Log("update vendor");
    }


    void OnEnable()
    {
        EventManager.itemSale += UpdateVendor;
    }
    void OnDisable()
    {
        EventManager.itemSale -= UpdateVendor;
    }

}
