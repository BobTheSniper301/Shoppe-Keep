using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript instance { get; private set; }
//     // AI variables
//     // Only pedestals with items
//     public List<GameObject> pedestals = new List<GameObject>();
//     public GameObject[] walkways;


//     public delegate void ItemSale();
//     public static ItemSale itemSale;

//     public delegate void GoldGained();
//     public static GoldGained goldGained;


//     public QuestEvents questEvents;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

//         questEvents = new QuestEvents();
    }
}
