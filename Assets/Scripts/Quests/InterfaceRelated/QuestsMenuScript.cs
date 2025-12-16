//using UnityEngine;
//using System.Collections.Generic;
//using UnityEngine.UI;

//public class QuestsMenuScript : MonoBehaviour
//{
//    public GameObject questContainer;

//    public GameObject questContainerHolder;
    
//    public List<GameObject> questContainers;
    

//    // public void DisplayQuests()
//    // {
//    //     foreach (QuestInfoSO activeQuest in QuestManager.instance.activeQuests)
//    //     {
//    //         GameObject _questContainer = Instantiate(questContainer, questContainerHolder.transform);
//    //         _questContainer.GetComponent<QuestContainer>().quest = activeQuest;
//    //         _questContainer.transform.Find("QuestName").GetComponent<Text>().text = activeQuest.id;

//    //         questContainers.Add(_questContainer);
//    //     }
//    // }


//    public void ClearQuestDisplay()
//    {
//        foreach (GameObject _questContainer in questContainers)
//        {
//            Debug.Log("delete");
//            Destroy(_questContainer);
//        }
//    }




//    public void OnEnable()
//    {
//        // DisplayQuests();
//    }

//    private void OnDisable()
//    {
//        ClearQuestDisplay();
//    }
//}
