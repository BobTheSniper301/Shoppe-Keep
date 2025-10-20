using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public bool placeable = true;
    public string name;

    public ItemType itemType;
    public enum ItemType
    {
        TOOL,
        ARMOR,
        STACKABLE
    }
}
