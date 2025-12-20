using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestsMenuScript : MonoBehaviour
{
   public GameObject questContainer;

   public GameObject questContainerHolder;
    
   public List<GameObject> questContainers;
    

   public void DisplayQuests()
   {
        // Debug.Log("displayQuests");
        // Debug.Log(QuestManager.instance);
        // Debug.Log(QuestManager.instance.activeQuests[0]);
        foreach (QuestInfoSO activeQuest in QuestManager.instance.activeQuests)
        {
            // Debug.Log("add container");
            GameObject _questContainer = Instantiate(questContainer, questContainerHolder.transform);
            _questContainer.GetComponent<QuestContainer>().quest = activeQuest;
            _questContainer.transform.Find("QuestName").GetComponent<Text>().text = activeQuest.id;

            questContainers.Add(_questContainer);
        }
   }


   public void ClearQuestDisplay()
   {
       foreach (GameObject _questContainer in questContainers)
       {
           Debug.Log("delete");
           questContainers.Remove(_questContainer);
           Destroy(_questContainer);
       }
   }




   public void OnEnable()
   {
       DisplayQuests();
   }

   private void OnDisable()
   {
       ClearQuestDisplay();
   }
}
