using UnityEngine;

public class VendorMenuScript : MonoBehaviour
{
    public GameObject menu;
    [SerializeField] MenuManager menuManager;
    public GameObject vendorItemUiPrefab;
    [SerializeField] GameObject purchasableItemsContainer;


    public void OpenMenu(VendorScript Vendor)
    {
        if (menuManager.activeMenu == null)
        {
            menu.SetActive(true);
            menuManager.activeMenu = menu;

            MakeItemUiPrefabs(Vendor);
        }
    }

    private void MakeItemUiPrefabs(VendorScript Vendor)
    {
        for (int i = 0; i <= Vendor.vendorData.unlockedWares; i++)
        {

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
