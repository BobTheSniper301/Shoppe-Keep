using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public bool placeable = true;


    public ItemType type;
    public enum ItemType
    {
        Tool,
        Armor,
        Stackable
    }
}
