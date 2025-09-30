using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIManager : MonoBehaviour
{
    public ItemScript[] items;
    public int[] itemSlotData = new int[] {0,0,0,0,0,0,0,0};

    void Start()
    {
        items = GetComponentsInChildren<ItemScript>();
        Debug.Log(items.Length);
        //foreach (Component item in items)
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log(itemSlotData.Length);
            Debug.Log(i);
            itemSlotData.SetValue(items[i].itemData.itemNum, i);
            Debug.Log(itemSlotData);

        }
    }

}
