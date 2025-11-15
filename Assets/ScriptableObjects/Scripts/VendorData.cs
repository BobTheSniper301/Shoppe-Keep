using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class MerchantData : ScriptableObject
{
    public int reputation;
    public int unlockedWares;
    public List<ScriptableObject> wares = new List<ScriptableObject>();
    
}
