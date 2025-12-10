using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;

    public float questProgress;
    
    
    public List<ItemData> rewards = new List<ItemData>();
    

    public enum ObjectiveStatus
    {
        INACTIVE,
        STARTED,
        ACTIVE,
        ACHIEVED,
        TURNEDIN,
        DONE
    }
}
