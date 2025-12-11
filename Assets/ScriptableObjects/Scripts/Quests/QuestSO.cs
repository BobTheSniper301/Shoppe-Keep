using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quests/Base Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string questDescription;

    public int[] questRequirementsInt;
    public string[] questRequirementsText;


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
