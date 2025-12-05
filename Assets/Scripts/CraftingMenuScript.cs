using UnityEngine;
using System.Threading.Tasks;

public class CraftingMenuScript : MonoBehaviour
{
    public ItemScript[] inputItems;
    public GameObject outputItem;
    public GameObject outputItemInst;


    public async void Craft()
    {
        PotionCraftingStationScript craftingStationScript = PlayerScript.instance.currentCraftingStation.gameObject.GetComponent<PotionCraftingStationScript>();
        await craftingStationScript.Craft();
        Debug.Log("done crafting");
        inputItems = this.GetComponentsInChildren<ItemScript>();
        outputItemInst = Instantiate(outputItem);
        UiManager.instance.SpawnGroundItem(outputItemInst, new Vector3(PlayerScript.instance.transform.position.x, 0, PlayerScript.instance.transform.position.z));
    }


    private void OnDisable()
    {
        
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
