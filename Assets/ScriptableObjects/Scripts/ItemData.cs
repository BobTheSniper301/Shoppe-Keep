using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public bool placeable = true;

    public ItemType itemType;
    public enum ItemType
    {
        Empty,
        Tool,
        Armor,
        Stackable
    }
}
