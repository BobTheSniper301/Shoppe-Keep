using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class SaveJson : MonoBehaviour
{
    // TODO: Change the way things are saved (kinda refactor)


    public static SaveJson instance { get; private set; }


    // PREFABS
    public GameObject itemPrefab;

    InventoryData _InventoryData = new InventoryData();
    [HideInInspector] string item;

    // Function to call to clear any json file if given the path.
    public static void ClearJsonFile(string filepath)
    {
        File.WriteAllText(filepath, string.Empty);
    }

    // Makes the saved items match the UiManager items
    public void SaveInventoryData()
    {

        // Clears file.
        ClearJsonFile(Application.persistentDataPath + "/InventoryData.json");

        // Ensures having an inventory list
        // Fills the list with blank objects blank values
        _InventoryData.inventoryItemDatas = new List<InventoryItemData>(8);
        InventoryItemData emptyObj = new InventoryItemData("null", "null", false, 0);
        for (int i = 0; i < UiManager.instance.items.Length; i++)
        {
            _InventoryData.inventoryItemDatas.Add(emptyObj);
        }


        // Updates each item with the right information
        for (int i = 0; i < _InventoryData.inventoryItemDatas.Count; i++)
        {
            if (UiManager.instance.itemsData[i] != null)
            {
                InventoryItemData item = new InventoryItemData(UiManager.instance.itemsData[i].itemName, UiManager.instance.itemsData[i].itemType.ToString(), UiManager.instance.itemsData[i].placeable, UiManager.instance.itemsData[i].count);
                _InventoryData.inventoryItemDatas[i] = item;
            }
        }

        // Updates the file
        item = JsonUtility.ToJson(_InventoryData);
        File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", item);
    }


    // Make the UiManager info, match the saved data
    public void LoadInventoryData()
    {
        // Text from the file
        string file = File.ReadAllText(Application.persistentDataPath + "/InventoryData.json");

        // Ensures having an inventory list
        if (file == "")
        {
            SaveInventoryData();
            return;

        }

        // Load the text into the class
        InventoryData loadData = JsonUtility.FromJson<InventoryData>(file);

        // Make the UiManager info, match the saved data
        for (int i = 0; i < UiManager.instance.itemsData.Length; i++)
        {
            if (UiManager.instance.itemsData[i] == null)
            {
                UiManager.instance.itemsData[i] = ScriptableObject.CreateInstance<ItemData>();
            }
            object empty;
            if (!Enum.TryParse(typeof(ItemData.ItemType), loadData.inventoryItemDatas[i]._itemType, out empty))
            {
                UiManager.instance.itemsData[i].itemType = ItemData.ItemType.EMPTY;
            }
            else
            {
                UiManager.instance.itemsData[i].itemType = (ItemData.ItemType)Enum.Parse(typeof(ItemData.ItemType), loadData.inventoryItemDatas[i]._itemType);
            }
            UiManager.instance.itemsData[i].itemName = loadData.inventoryItemDatas[i]._name;
            UiManager.instance.itemsData[i].placeable = loadData.inventoryItemDatas[i]._placeable;
            UiManager.instance.itemsData[i].count = loadData.inventoryItemDatas[i]._count;

        }

    }

    public void Save()
    {
        // Updates UiManager.instance.items list then saves the data of the list and each item's data
        #region Inventory Save

        UiManager.instance.GetItems();

        SaveInventoryData();

        #endregion

    }


    public void Load()
    {

        // Gets the saved inventory data; Creates all the new UiManager.instance.items with the appropriate data, transform, etc
        #region Inventory Load

        UiManager.instance.ClearInventory();

        LoadInventoryData();

        for (int i = 0; i < UiManager.instance.itemsData.Length; i++)
        {

            // If it's not a blank item, make the item
            if (UiManager.instance.itemsData[i] != null && UiManager.instance.itemsData[i].itemName != "null")
            {

                GameObject newItem = Instantiate(itemPrefab, UiManager.instance.itemSlots[i].transform);
                newItem.GetComponent<ItemScript>().itemData = UiManager.instance.itemsData[i];
                newItem.GetComponent<ItemScript>().itemData.name = UiManager.instance.itemsData[i].itemName;
                newItem.name = UiManager.instance.itemsData[i].itemName;
                newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImages/" + newItem.name);
                newItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ItemImages/" + newItem.name);
                UiManager.instance.items[i] = newItem.GetComponent<ItemScript>();
            }

        }

        // ToDo: make this actually run on the next frame
        // Calls this the "next frame" to ensure the UiManager.instance.items are moved correctly when the itemslots are updated
        UiManager.instance.Invoke("GetItems", 0.000001f);


        #endregion
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


}

[Serializable]
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

[Serializable]
public class InventoryData
{
    public List<InventoryItemData> inventoryItemDatas;
}