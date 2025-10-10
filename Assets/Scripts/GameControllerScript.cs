using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    //[HideInInspector] public GameObject[] heldObjects;
    public Transform[] heldObjects;
    private void Start()
    {
        //heldObjects = GameObject.FindGameObjectsWithTag("HeldItem");

        Transform[] tempHeldObjects = GameObject.Find("Player/HeldItems").GetComponentsInChildren<Transform>(includeInactive:true);
        System.Array.ConstrainedCopy(tempHeldObjects, 1, heldObjects, 0, tempHeldObjects.Length - 1);
        foreach( Transform t in heldObjects)
        {
            Debug.Log(t.gameObject.name);
        }
    }
}
