using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class SaveJson : MonoBehaviour
{
    private InventoryData _InventoryData = new InventoryData();
    [HideInInspector] string item;
    public UiManager uiManager;

    // Function to call to clear any json file if given the path.
    public static void ClearJsonFile(string filepath)
    {
        Debug.Log("clear");
        System.IO.File.WriteAllText(filepath, string.Empty);
    }


    public void SaveInventoryData()
    {


        // Clears file.
        ClearJsonFile(Application.persistentDataPath + "/InventoryData.json");

        _InventoryData.inventoryItemDatas = new List<InventoryItemData>(8);
        InventoryItemData emptyObj = new InventoryItemData("null", "null", false, 0);
        for (int i = 0; i < uiManager.items.Length; i++)
        {
            _InventoryData.inventoryItemDatas.Add(emptyObj);
        }


        // Updates each item with the right information
        for (int i = 0; i < _InventoryData.inventoryItemDatas.Count; i++)
        {
            if (uiManager.itemsData[i] != null)
            {
                InventoryItemData item = new InventoryItemData(uiManager.itemsData[i].itemName, uiManager.itemsData[i].itemType.ToString(), uiManager.itemsData[i].placeable, uiManager.itemsData[i].count);
                _InventoryData.inventoryItemDatas[i] = item;
            }
        }

        item = JsonUtility.ToJson(_InventoryData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
    }


    // REMEMBER TO START THE PLAYER WITH AN EMPTY SAVE FILE AT LEAST
    public void LoadInventoryData()
    {
        string file = File.ReadAllText(Application.persistentDataPath + "/InventoryData.json");
        InventoryData loadData = JsonUtility.FromJson<InventoryData>(file);

        for (int i = 0; i < uiManager.itemsData.Length; i++)
        {
            if (uiManager.itemsData[i] == null)
            {
                uiManager.itemsData[i] = ScriptableObject.CreateInstance<ItemData>();
            }
            object empty;
            if (!Enum.TryParse(typeof(ItemData.ItemType), loadData.inventoryItemDatas[i]._itemType, out empty))
            {
                uiManager.itemsData[i].itemType = ItemData.ItemType.EMPTY;
            }
            else
            {
                uiManager.itemsData[i].itemType = (ItemData.ItemType)Enum.Parse(typeof(ItemData.ItemType), loadData.inventoryItemDatas[i]._itemType);
            }
            uiManager.itemsData[i].itemName = loadData.inventoryItemDatas[i]._name;
            uiManager.itemsData[i].placeable = loadData.inventoryItemDatas[i]._placeable;
            uiManager.itemsData[i].count = loadData.inventoryItemDatas[i]._count;

        }

    }
}

[System.Serializable]
public class InventoryItemData
{
    public string _name;
    public string _itemType;
    public bool _placeable;
    public int _count;

    public InventoryItemData(string name, string itemType, bool placeable, int count)
    {
        _name = name;
        _itemType = itemType;
        _placeable = placeable;
        _count = count;
    }

}

[System.Serializable]
public class InventoryData
{
    // public string blank = "hi";
    public List<InventoryItemData> inventoryItemDatas;
}