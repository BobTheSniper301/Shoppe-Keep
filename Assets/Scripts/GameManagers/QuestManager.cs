using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }

    private Dictionary<string, Quest> questMap;

    public List<QuestInfoSO> activeQuests;


    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameControllerScript.instance.questEvents.QuestStateChange(quest);
    }

    
    private bool CheckRequirementsMet(Quest quest)
    {
        // Start true, prove false
        bool meetsRequirements = true;

        // Check quest prereqs for completion
        foreach (QuestInfoSO prereqQuestInfo in quest.info.questPrereqs)
        {
            if (GetQuestById(prereqQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }


    private void StartQuest(string id)
    {
        Debug.Log("Start Quest: " + id);
        Quest quest = GetQuestById(id);
        quest.InstatiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        activeQuests.Add(quest.info);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        // Move on to the next step
        quest.MoveToNextStep();

        // If there are more steps, instatiate the next one
        if (quest.CurrentStepExists())
        {
            quest.InstatiateCurrentQuestStep(this.transform);
        }
        // If there are no more steps, then we've finished all of them for this quest
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Debug.Log("Finish Quest: " + id);
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
        activeQuests.Remove(quest.info);
    }


    private void ClaimRewards(Quest quest)
    {
        PlayerScript.instance.playerData.gold += quest.info.goldReward;
        GameControllerScript.goldGained();
        foreach (GameObject item in quest.info.itemRewards)
        {
            UiManager.instance.SpawnGroundItem(item, new Vector3(PlayerScript.instance.transform.position.x, 0, PlayerScript.instance.transform.position.z));
        }
        Debug.Log("claim rewards");
    }


    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        // Create the quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            Debug.Log(questInfo.name);
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Dupicate ID found when creating quest map: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    // Mostly just function as a fail safe to ensure that the quest does exist, so we will use this to get a quest by id instead of referencing it directly
    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }
        return quest;
    }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        questMap = CreateQuestMap();
    }

    void OnEnable()
    {
        GameControllerScript.instance.questEvents.onStartQuest += StartQuest;
        GameControllerScript.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameControllerScript.instance.questEvents.onFinishQuest += FinishQuest;
    }


    public void Start()
    {
        // Broadcast the initial state of all quests on startup
        foreach (Quest quest in questMap.Values)
        {
            GameControllerScript.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void Update()
    {
        // Maybe consider making this a function and maybe not have it go through update and instead be based upon an event?
        // Loop through all quests
        foreach (Quest quest in questMap.Values)
        {
            // If we're now meeting the requirements, switch over to the CAN_START  state
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }
} 
  
//     public delegate void QuestChange(QuestUpdateType questUpdateType);
//     public static QuestChange questChange;

    

//     public QuestInfoSO[] allQuests;

//     public List<QuestInfoSO> activeQuests;

//     public List<AllQuestProgresses> allQuestProgresses;

//     public enum QuestUpdateType
//     {
//         Sale,
//         Pickup,
//         Dialogue,
//         Interaction
//     }



//     public void StoreQuestData()
//     {
//         foreach (QuestInfoSO quest in activeQuests)
//         {
            



//             // List<int> temp_questProgress =  new List<int>();
//             // foreach (int questProgress in containerScript.questProgress)
//             // {
//             //     temp_questProgress.Add(questProgress);
//             // }

//             // allQuestProgresses.Add(new AllQuestProgresses(containerScript.quest.questName, temp_questProgress));
//         }
//     }


//     void Update()
//     {
//         if (Input.GetKeyDown("space"))
//         {
//             foreach (AllQuestProgresses questProgress in allQuestProgresses)
//             {
//                 Debug.Log("quest: " + questProgress);
//             }
//         }
//     }

// }

// [Serializable]
// public class AllQuestProgresses
// {
//     public List<int> _questProgress;
//     public string _questName;

//     public AllQuestProgresses(string questName, List<int> questProgress)
//     {
//         _questName = questName;
//         _questProgress = questProgress;
//     }
// }
