using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    //[HideInInspector] public GameObject[] heldObjects;
    public Transform[] heldObjects;
    private void Start()
    {

        // Get items from list, starting at a specific value
        // Gets our held items
        // Remember to update array in inspector when adding a new item
        Transform[] tempHeldObjects = GameObject.Find("Player/Main Camera/HeldItems").GetComponentsInChildren<Transform>(includeInactive:true);
        System.Array.ConstrainedCopy(tempHeldObjects, 1, heldObjects, 0, tempHeldObjects.Length - 1);
        
        // foreach( Transform t in heldObjects)
        // {
        // Debug.Log(t.gameObject.name);
        // }
    }
}
