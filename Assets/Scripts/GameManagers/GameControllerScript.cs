using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    // AI variables
    // Only pedestals with items
    public List<GameObject> pedestals = new List<GameObject>();
    public GameObject[] walkways;



    public Transform[] heldObjects;


    public delegate void ItemPurchased();
    public static ItemPurchased itemPurchased;


    public static GameControllerScript instance { get; private set; }

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
    }


    void Start()
    {

        // Gets our held items
        // Remember to update array in inspector when adding a new item
        Transform[] tempHeldObjects = GameObject.Find("Player/PlayerCamera/HeldItems").GetComponentsInChildren<Transform>(includeInactive:true);
        // Get items from list, starting at a specific value
        System.Array.ConstrainedCopy(tempHeldObjects, 1, heldObjects, 0, tempHeldObjects.Length - 1);
    }
}
