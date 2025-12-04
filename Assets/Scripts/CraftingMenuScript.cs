using UnityEngine;

public class CraftingMenuScript : MonoBehaviour
{
    public void Craft()
    {
        PotionCraftingStationScript craftingStationScript = PlayerScript.instance.currentCraftingStation.gameObject.GetComponent<PotionCraftingStationScript>();
        craftingStationScript.Craft();
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
