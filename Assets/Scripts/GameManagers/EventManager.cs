using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance { get; private set; }

    public delegate void ItemSale();
    public static ItemSale itemSale;

    public delegate void GoldGained();
    public static GoldGained goldGained;

    public delegate void PedestalChanged();
    public static PedestalChanged pedestalChanged;

    public delegate void GameStart();
    public static GameStart gameStart;


    // public QuestEvents questEvents;



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
