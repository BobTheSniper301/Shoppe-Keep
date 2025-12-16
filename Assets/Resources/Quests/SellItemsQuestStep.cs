using UnityEngine;

public class SellItemsQuestStep : QuestStep
{
    private int itemsSold = 0;

    private int salesToComplete = 2;

    void OnEnable()
    {
        GameControllerScript.itemSale += ItemSold;
    }

    void OnDisable()
    {
        GameControllerScript.itemSale -= ItemSold;
    }

    private void ItemSold()
    {
        if (itemsSold < salesToComplete)
        {
            itemsSold++;
        }
        if (itemsSold >= salesToComplete)
        {
            FinishQuestStep();
        }
    }
}
