using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestInfoSO questInfoForTrig;

    private string questId;
    private QuestState currentQuestState;

    void Awake()
    {
        questId = questInfoForTrig.id;
    }


    void OnEnable()
    {
        GameControllerScript.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    void OnDisable()
    {
        GameControllerScript.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        // Only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            Debug.Log("Quest with Id: " + questId + " updated to state: " + currentQuestState);
        }
    }
}
