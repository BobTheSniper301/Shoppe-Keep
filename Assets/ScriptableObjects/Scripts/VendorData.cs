using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class VendorData : ScriptableObject
{
    public int reputation;

    public string vendorName;
    public string baseDialogue;

    public int unlockedWares;
    public List<ItemData> wares = new List<ItemData>();

}
