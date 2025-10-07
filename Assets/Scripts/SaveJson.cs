using UnityEngine;

public class SaveJson : MonoBehaviour
{
    //public UIManager uiManager;
    private InventoryData _InventoryData = new InventoryData();


    public void SaveInventoryData()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", string.Empty);
        _InventoryData._items = GetComponent<UIManager>().items;
        string item = JsonUtility.ToJson(_InventoryData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
        Debug.Log(item);
    }


}
public class InventoryData
{
    public ItemScript[] _items;
}