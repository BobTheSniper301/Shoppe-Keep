using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public bool placeable = false;
    public string itemName = "null";
    public int count = 1;
    public int price;

    public ItemType itemType;
    public enum ItemType
    {
        EMPTY,
        TOOL,
        ARMOR,
        STACKABLE
    }
}
