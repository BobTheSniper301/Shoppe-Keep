// // using UnityEngine;
// using System.Collections.Generic;

// [CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quests/Base Quest")]
// public class QuestInfoSO : ScriptableObject
// {
//     public string id;
    
//     [Header("General")]
//     public string displayName;

//     [Header("Requirements")]
//     public QuestInfoSO[] questPrereqs;
    
//     [Header("Steps")]
//     public GameObject[] questStepPrefabs;

//     [Header("Rewards")]
//     public GameObject[] itemRewards;
//     public int goldReward;
//     public int renownReward;



//     private void OnValidate()
//     {
//         #if UNITY_EDITOR
//         id = this.name;
//         UnityEditor.EditorUtility.SetDirty(this);
//         #endif
//     }


//     public string questDescription;

//     public int[] questRequirementsInt;
//     public string[] questRequirementsText;

// }
