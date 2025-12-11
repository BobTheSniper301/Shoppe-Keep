using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }

    public delegate void QuestChange();
    public static QuestChange questChange;


    public List<Quest> allQuests = new List<Quest>();




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
