using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class SaveJson : MonoBehaviour
{
    private InventoryData _InventoryData = new InventoryData();
    [HideInInspector] string item;


    // Function to call to clear any json file if given the path.
    public static void ClearJsonFile(string filepath)
    {
        System.IO.File.WriteAllText(filepath, string.Empty);
    }

    //     public void SaveInventoryData()
    //     {
    //         // Clears file.
    //         ClearJsonFile(Application.persistentDataPath + "/InventoryData.json");

    //         _InventoryData.inventoryItemDatas = GetComponent<UiManager>().itemsData;
    //         _InventoryData._stackableNums = GetComponent<UiManager>().stackableNums;
    //         item = JsonUtility.ToJson(_InventoryData);
    //         System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
    //         Debug.Log(item);
    //     }

    // // REMEMBER TO START THE PLAYER WITH AN EMPTY SAVE FILE AT LEAST
    //     public void LoadInventoryData()
    //     {
    //         string filePath = File.ReadAllText(Application.persistentDataPath + "/InventoryData.json");
    //         InventoryData loadData = JsonUtility.FromJson<InventoryData>(filePath);
    //         GetComponent<UiManager>().itemsData = loadData.inventoryItemDatas;
    //         GetComponent<UiManager>().stackableNums = loadData._stackableNums;
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
        Debug.Log(_InventoryData.inventoryItemDatas);

        if (_InventoryData.inventoryItemDatas == null)
        {
            _InventoryData.inventoryItemDatas = new List<InventoryItemData>(8);
            InventoryItemData emptyObj = new InventoryItemData("null", 0);
            for (int i = 0; i < GetComponent<UiManager>().items.Length; i++)
            {
                 _InventoryData.inventoryItemDatas.Add(emptyObj);
            }
        }

        // Debug.Log(_InventoryData.inventoryItemDatas);
        // Debug.Log("Count: " + _InventoryData.inventoryItemDatas.Count);
        // Debug.Log("at 0: " + _InventoryData.inventoryItemDatas[0]);
        // Debug.Log("name at 0: " + _InventoryData.inventoryItemDatas[0]._name);
        // Debug.Log("itemData at 1, name: " + GetComponent<UiManager>().itemsData[1].name);

        // Updates each item with the right information
        for (int i = 0; i < _InventoryData.inventoryItemDatas.Count; i++)
        {
            // Debug.Log("update with info");
            // Debug.Log("i: " + i);
            
            if (GetComponent<UiManager>().itemsData[i] != null)
            {
                // Debug.Log("hi");
                // Debug.Log(GetComponent<UiManager>().itemsData[i].name);
                _InventoryData.inventoryItemDatas[i]._name = GetComponent<UiManager>().itemsData[i].name;
                _InventoryData.inventoryItemDatas[i]._itemType = (int)GetComponent<UiManager>().itemsData[i].itemType;
            }
        }

        // Debug.Log("at 0: " + _InventoryData.inventoryItemDatas[0]);
        // Debug.Log("name at 0: " + _InventoryData.inventoryItemDatas[0]._name);

        _InventoryData._stackableNums = GetComponent<UiManager>().stackableNums;

        item = JsonUtility.ToJson(_InventoryData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
        // Console.WriteLine(item);

        Debug.Log("item: " + item);
    }


    public void LoadInventoryData()
    {

    }
}

[System.Serializable]
public class InventoryItemData
{
    public string _name;
    public int _itemType;

    public InventoryItemData(string name, int itemType)
    {
        _name = name;
        _itemType = itemType;
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