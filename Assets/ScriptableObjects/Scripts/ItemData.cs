using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public bool placeable = true;


    public ItemType itemType;
    public enum ItemType
    {
        Tool,
        Armor,
        Stackable
    }
}
