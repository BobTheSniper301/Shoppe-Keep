using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIManager : MonoBehaviour
{
    public ItemScript[] items;
    public ItemSlotScript[] itemSlots;
    public SaveJson saveJson;


    public void getItems()
    {
        // Gets a list of all item slots
        int x = 0;
        //itemSlots = GetComponentsInChildren<ItemSlotScript>();
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
    }


    public void Save()
    {
        getItems();
        GetComponent<SaveJson>().SaveInventoryData();


    }
}
