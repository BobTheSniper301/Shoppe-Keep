using UnityEngine;

public class SaveJson : MonoBehaviour
{
    //public UIManager uiManager;
    private InventoryData _InventoryData = new InventoryData();
    [HideInInspector] string item;

    public void SaveInventoryData()
    {
        // Clears file.
        ClearJsonFile(Application.persistentDataPath + "/InventoryData.json");
        
        _InventoryData._items = GetComponent<UIManager>().items;
        item = JsonUtility.ToJson(_InventoryData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
        Debug.Log(item);
    }

    // public void LoadInventoryData()
    // {
    //     GetComponent<UIManager>().items = JsonUtility.FromJson<ItemScript[]>(item);
    // }

    
    // Function to call to clear any json file if given the path.
    public static void ClearJsonFile(string filepath)
    {
        System.IO.File.WriteAllText(filepath, string.Empty);
    }

}
public class InventoryData
{
    public ItemScript[] _items;
}