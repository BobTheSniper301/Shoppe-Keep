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
        System.IO.File.WriteAllText(filepath, string.Empty);
    }

    //     public void SaveInventoryData()
    //     {
    //         // Clears file.
    //         ClearJsonFile(Application.persistentDataPath + "/InventoryData.json");

    //         _InventoryData.inventoryItemDatas = uiManager.itemsData;
    //         _InventoryData._stackableNums = uiManager.stackableNums;
    //         item = JsonUtility.ToJson(_InventoryData);
    //         System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
    //         Debug.Log(item);
    //     }

    // // REMEMBER TO START THE PLAYER WITH AN EMPTY SAVE FILE AT LEAST
    //     public void LoadInventoryData()
    //     {
    //         string filePath = File.ReadAllText(Application.persistentDataPath + "/InventoryData.json");
    //         InventoryData loadData = JsonUtility.FromJson<InventoryData>(filePath);
    //         uiManager.itemsData = loadData.inventoryItemDatas;
    //         uiManager.stackableNums = loadData._stackableNums;
    //     }

    // public void SaveInventoryData(int[][] something)
    // {
    //     ClearJsonFile(Application.persistentDataPath + "/InventoryData.json");

    //     InventoryData wrapper = new InventoryData();
    //     wrapper.inventoryItemDatas = new List<InventoryItemData>();

    //     int i = 0;
    //     while (i < something.Length)
    //     {
    //         InventoryItemData innerWrapper = new InventoryItemData();
    //         innerWrapper.name
    //     }
    // }

    public void SaveInventoryData()
    {


        // Clears file.
        ClearJsonFile(Application.persistentDataPath + "/InventoryData.json");

        // Debug.Log(_InventoryData);
        // Debug.Log(_InventoryData.inventoryItemDatas);

        if (_InventoryData.inventoryItemDatas == null)
        {
            _InventoryData.inventoryItemDatas = new List<InventoryItemData>(8);
            InventoryItemData emptyObj = new InventoryItemData("null", "null", false);
            for (int i = 0; i < uiManager.items.Length; i++)
            {
                 _InventoryData.inventoryItemDatas.Add(emptyObj);
            }
        }

        // Debug.Log(_InventoryData.inventoryItemDatas);
        // Debug.Log("Count: " + _InventoryData.inventoryItemDatas.Count);
        // Debug.Log("at 0: " + _InventoryData.inventoryItemDatas[0]);
        // Debug.Log("name at 0: " + _InventoryData.inventoryItemDatas[0]._name);
        // Debug.Log("itemData at 1, name: " + uiManager.itemsData[1].name);

        // Updates each item with the right information
        for (int i = 0; i < _InventoryData.inventoryItemDatas.Count; i++)
        {
            // Debug.Log("update with info");
            // Debug.Log("i: " + i);
            // Debug.Log("before: " + _InventoryData.inventoryItemDatas[3]._itemType);
            if (uiManager.itemsData[i] != null)
            {
                // Debug.Log("hi");
                // Debug.Log("itemdata at i: " + uiManager.itemsData[i]);
                // Debug.Log(" i: " + i);
                InventoryItemData item = new InventoryItemData(uiManager.itemsData[i].itemName, uiManager.itemsData[i].itemType.ToString(), uiManager.itemsData[i].placeable);
                _InventoryData.inventoryItemDatas[i] = item;
                // _InventoryData.inventoryItemDatas[i]._name = uiManager.itemsData[i].name;
                // _InventoryData.inventoryItemDatas[i]._itemType = uiManager.itemsData[i].itemType.ToString();
            }
            else
                break;
            // Debug.Log("after: " + _InventoryData.inventoryItemDatas[3]._itemType);
        }
        // Debug.Log("did not leave");
        // Debug.Log("at 0: " + _InventoryData.inventoryItemDatas[0]);
        // Debug.Log("name at 0: " + _InventoryData.inventoryItemDatas[0]._name);

        _InventoryData._stackableNums = uiManager.stackableNums;

        item = JsonUtility.ToJson(_InventoryData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
        // Console.WriteLine(item);

        // Debug.Log("item: " + item);
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

            // uiManager.itemsData[i].itemType = (ItemData.ItemType)Enum.Parse(typeof(ItemData.ItemType), loadData.inventoryItemDatas[i]._itemType);
            // Debug.Log(uiManager.itemsData[i]);

        }


        // uiManager.itemsData = loadData.inventoryItemDatas;
        // uiManager.stackableNums = loadData._stackableNums;
    }
}

[System.Serializable]
public class InventoryItemData
{
    public string _name;
    public string _itemType;
    public bool _placeable;

    public InventoryItemData(string name, string itemType, bool placeable)
    {
        _name = name;
        _itemType = itemType;
        _placeable = placeable;
    }

    // public InventoryItemData(string name, Enum itemType, Image image, Sprite sprite)
    // {
    //     _name = name;
    //     _itemType = itemType;
    //     _image = image;
    //     _sprite = sprite;
    // }

}

[System.Serializable]
public class InventoryData
{
    // public string blank = "hi";
    public List<InventoryItemData> inventoryItemDatas;
    public string[] _stackableNums;
}