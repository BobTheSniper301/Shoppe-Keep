using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class UIManager : MonoBehaviour
{
    // private InventoryData inventoryData;

    // All items the player has and at index matching the hotbar
    public ItemScript[] items;

    public ItemSlotScript[] itemSlots;

    // Gets all items in the hotbar and saves them to items[] at an index matching the hotbar slot
    void GetItems()
    {
        // Gets a list of all item slots
        int x = 0;
        itemSlots = GetComponentsInChildren<ItemSlotScript>();
        // Check each of the items slots to see if they have an item in them
        foreach (ItemSlotScript i in itemSlots)
        {
            //If it does have an item, if it does, it stores it into the items list at the proper index
            if (i.GetComponentInChildren<ItemScript>())
            {
                items[x] = i.GetComponentInChildren<ItemScript>();
            }
            //Debug.Log(string.Join(", ", items[x]));
            x++;
        }

        // // Prints all items within the items list
        // int z = 0;
        // while (z < items.Length)
        // {
        //     Debug.Log(items[z]);
        //     z++;
        // }
    }

    // void SetInventoryData(int i)
    // {
    //     inventoryData.SavedItems[i] = items[i];   
    // }

    public void Save()
    {
        GetItems();
        // int x = 0;
        // while (x < items.Length)
        // {
        //     inventoryData.SavedItems[x] = items[x];
        // }
        // Debug.Log(inventoryData.SavedItems + "hi");
        Debug.Log(items + "hi");

        // inventoryData.SavedItems = items;

        

        // string json = JsonUtility.ToJson(inventoryData);
        // Debug.Log(json);

        using (StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "InventoryData.json"))
        {
            // writer.Write(json);
        }

    }


    void Load()
    {
        string json = string.Empty;

        using (StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "InventoryData.json"))
        {
            json = reader.ReadToEnd();
        }

        InventoryData data = JsonUtility.FromJson<InventoryData>(json);
        
        // items = inventoryData.SavedItems; 
    }


    private void Start()
    {
        Load();
    }

}
