using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public bool placeable = false;
    public string itemName = "null";
    public float price;

    public ItemType itemType;
    public enum ItemType
    {
        EMPTY,
        TOOL,
        ARMOR,
        STACKABLE
    }
}
