using UnityEngine;

public class VendorScript : MonoBehaviour
{
    private GameObject vendorMenu;

    public VendorData vendorData;


    public void UpdateVendor()
    {
        Debug.Log("update vendor");
    }


    void OnEnable()
    {
        GameControllerScript.itemSale += UpdateVendor;
    }
    void OnDisable()
    {
        GameControllerScript.itemSale -= UpdateVendor;
    }



    void Start()
    {
        vendorMenu = UiManager.instance.vendorMenu;
    }


    void Update()
    {
        
    }
}
