using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public bool placeable = true;
    [Range(1,8)]public int itemNum;

    public GameObject itemToDisplay;


    public ItemType type;
    public enum ItemType
    {
        Tool,
        Armor,
        Stackable
    }
}
