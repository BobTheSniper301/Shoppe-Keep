using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }

    public delegate void QuestChange();
    public static QuestChange questChange;

    public GameObject questContainer;

    public GameObject questContainerHolder;

    public List<Quest> allQuests = new List<Quest>();

    public List<Quest> activeQuests = new List<Quest>();

    public List<GameObject> questContainers = new List<GameObject>();


    public void DisplayQuests()
    {
        foreach (Quest activeQuest in activeQuests)
        {
            GameObject _questContainer = Instantiate(questContainer, questContainerHolder.transform);
            _questContainer.GetComponent<QuestContainer>().quest = activeQuest;
            _questContainer.transform.Find("QuestName").GetComponent<Text>().text = activeQuest.questName;

            questContainers.Add(_questContainer);
        }
    }


    public void ClearQuestDisplay()
    {
        foreach (GameObject _questContainer in questContainers)
        {
            Debug.Log("delete");
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
