using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [HideInInspector] GameObject container;
    public GameObject player;
    public PlayerScript playerScript;

    public Transform[] heldObjects;


    void Start()
    {

        // Gets our held items
        // Remember to update array in inspector when adding a new item
        Transform[] tempHeldObjects = GameObject.Find("Player/PlayerCamera/HeldItems").GetComponentsInChildren<Transform>(includeInactive:true);
        // Get items from list, starting at a specific value
        System.Array.ConstrainedCopy(tempHeldObjects, 1, heldObjects, 0, tempHeldObjects.Length - 1);
    }
}
