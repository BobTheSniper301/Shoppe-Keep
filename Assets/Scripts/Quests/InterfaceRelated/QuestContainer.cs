using UnityEngine;

public class QuestContainer : MonoBehaviour
{

    public Quest quest;

    // 0, or 2, or whatever to give a result of 0/10 (10 being the requirement number) or 2/10
    public int[] questProgress;

    public float questPercent;


    public void ShowQuestDetails()
    {
        UiManager.instance.MenuOpen(UiManager.instance.questDetailsMenu);
        Debug.Log("menu open");
        UiManager.instance.questDetailsMenu.GetComponent<QuestDetailsMenu>().questDescription.text = quest.questDescription;
        UiManager.instance.questDetailsMenu.GetComponent<QuestDetailsMenu>().questName.text = quest.questName;
        
        int i = 0;
        UiManager.instance.questDetailsMenu.GetComponent<QuestDetailsMenu>().questObjectives.text = quest.questRequirementsText[0] + ": " + questProgress[0] + "/" + quest.questRequirementsInt[i] + "\n";
        foreach (string questRequirement in quest.questRequirementsText)
        {
            if (i > 0)
            {
                Debug.Log("run");
                UiManager.instance.questDetailsMenu.GetComponent<QuestDetailsMenu>().questObjectives.text += questRequirement + ": " + questProgress[i] + "/" + quest.questRequirementsInt[i] + "\n";

            }
            i++;
        }
        
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
