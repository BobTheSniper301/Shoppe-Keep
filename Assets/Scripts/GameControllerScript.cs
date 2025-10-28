using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [HideInInspector] GameObject container;
    public GameObject player;
    public PlayerScript playerScript;

    public Transform[] heldObjects;


    // Ex:PlayerStatChange(playerScript.playerData.maxHealth, 10);
    public void PlayerStatChange(float stat, float statValue)
    {
        stat = statValue;
    }

    // Button BS
    public void ButtonIncreasePlayerMaxHealth(float statChange)
    {
        PlayerStatChange(playerScript.playerData.maxHealth, playerScript.playerData.maxHealth + 1f);
    }
    public void ButtonDecreasePlayerMaxHealth(float statChange)
    {
        PlayerStatChange(playerScript.playerData.maxHealth, playerScript.playerData.maxHealth - 1f);
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
