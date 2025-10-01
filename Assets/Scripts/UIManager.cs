using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIManager : MonoBehaviour
{
    public ItemScript[] items;
    public ItemSlotScript[] itemSlots;

    public void Save()
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


        // Checks each thing in items array, if it's an item it checks the items slot number and if it doesn't match it removes it
        int y = 1;
        while (y < items.Length)
        {
            if (items[y] is ItemScript)
            {
                if (items[y].itemData.itemNum != y)
                {
                    items[y - 1] = null;
                }
            }
            y++;
        }

        // Prints all items within the items list
        int z = 0;
        while (z < items.Length)
        {
            //Debug.Log(items[z]);
            z++;
        }
    }    

    void Start()
    {

    }

}
