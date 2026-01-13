using System.Collections.Generic;
using UnityEngine;


public class AiManager : MonoBehaviour
{
    public static AiManager instance { get; private set; }

    // Only pedestals with items
    public List<GameObject> pedestals = new List<GameObject>();
    public GameObject[] walkways;
    
    
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
}
