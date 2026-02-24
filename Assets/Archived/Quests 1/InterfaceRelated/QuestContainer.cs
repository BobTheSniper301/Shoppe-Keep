// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class QuestContainer : MonoBehaviour
// {

//     public QuestInfoSO quest;

//     // 0, or 2, or whatever to give a result of 0/10 (10 being the requirement number) or 2/10
//     public List<int> questProgress;

//     public float questPercent;

//     [SerializeField] Text questPercentText;


//     public void FinishQuest()
//     {
//         GameControllerScript.instance.questEvents.FinishQuest(quest.id);
//     }
//     public void StartQuest()
//     {
//         GameControllerScript.instance.questEvents.StartQuest(quest.id);
//     }


//     public void ShowQuestDetails()
//     {
//         UiManager.instance.MenuOpen(UiManager.instance.questDetailsMenu);
//         Debug.Log("menu open");
//         UiManager.instance.questDetailsMenu.GetComponent<QuestDetailsMenu>().questDescription.text = quest.questDescription;
//         UiManager.instance.questDetailsMenu.GetComponent<QuestDetailsMenu>().questName.text = quest.id;
        
//         //int i = 0;
//         //// Clears the text and then we change/add on to it
//         //UiManager.instance.questDetailsMenu.GetComponent<QuestDetailsMenu>().questObjectives.text = "";
//         //foreach (string questRequirement in quest.questRequirementsText)
//         //{
//         //    Debug.Log("run");
//         //    UiManager.instance.questDetailsMenu.GetComponent<QuestDetailsMenu>().questObjectives.text += questRequirement + ": " + questProgress[i] + "/" + quest.questRequirementsInt[i] + "\n";
//         //    i++;
//         //}
        
//     }

//     public void UpdateShownQuestPercent()
//     {
//         questPercentText.text = questPercent.ToString();
//         questPercentText.text += "%";
//     }

//     void OnEnable()
//     {
//         //UpdateShownQuestPercent();
//     }

// }
