using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveJson : MonoBehaviour
{
    private InventoryData _InventoryData = new InventoryData();
    [HideInInspector] string item;

    public void SaveInventoryData()
    {
        // Clears file.
        ClearJsonFile(Application.persistentDataPath + "/InventoryData.json");
        
        _InventoryData._items = GetComponent<UiManager>().items;
        _InventoryData._stackableNums = GetComponent<UiManager>().stackableNums;
        item = JsonUtility.ToJson(_InventoryData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
        Debug.Log(item);
    }

    public void LoadInventoryData()
    {
        string asdf = File.ReadAllText(item);
        InventoryData loadData = JsonUtility.FromJson<InventoryData>(asdf);
        Debug.Log(loadData._items.Length);
        GetComponent<UiManager>().items = loadData._items;  
        Debug.Log(GetComponent<UiManager>().items.Length);
    }


    // Function to call to clear any json file if given the path.
    public static void ClearJsonFile(string filepath)
    {
        System.IO.File.WriteAllText(filepath, string.Empty);
    }

}

public class InventoryData
{
    public ItemScript[] _items;
    public Text[] _stackableNums;
}