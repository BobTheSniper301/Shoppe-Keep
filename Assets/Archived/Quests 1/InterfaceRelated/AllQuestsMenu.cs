 using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AllQuestsMenu : MonoBehaviour
{
    //     public GameObject questContainer;

    //     public GameObject questContainerHolder;

    //     public List<GameObject> questContainers;

    //     private QuestInfoSO[] allQuests;


    //      public void DisplayQuests()
    //     {
    //         allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
    //         foreach (QuestInfoSO quest in allQuests)
    //         {
    //             GameObject _questContainer = Instantiate(questContainer, questContainerHolder.transform);
    //             _questContainer.GetComponent<QuestContainer>().quest = quest;
    //             _questContainer.transform.Find("QuestName").GetComponent<Text>().text = quest.id;

    //             questContainers.Add(_questContainer);
    //         }
    //     }


    //     public void ClearQuestDisplay()
    //     {
    //         foreach (GameObject _questContainer in questContainers)
    //         {
    //             Debug.Log("delete");
    //             Destroy(_questContainer);
    //         }
    //     }




    //     public void OnEnable()
    //     {
    //          DisplayQuests();
    //     }

    //     private void OnDisable()
    //     {
    //         ClearQuestDisplay();
    //     }
}
