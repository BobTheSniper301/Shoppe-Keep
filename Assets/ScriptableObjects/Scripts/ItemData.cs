using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public bool placeable = false;
    public string itemName = "null";

    public ItemType itemType;
    public enum ItemType
    {
        EMPTY,
        TOOL,
        ARMOR,
        STACKABLE
    }

    // public void CreateItemData(bool placeable, string name, string ItemType)
    // {
        
    // }
}
