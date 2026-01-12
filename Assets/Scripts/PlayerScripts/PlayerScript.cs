using UnityEngine;

// Will create one if we don't have one
[RequireComponent(typeof(CharacterController))]

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance { get; private set;  }

    // Player stats
    public float currentHealth;
    public float maxHealth;
    public float gold;

    public int priceChangePower = 1;

    



//     public GameObject currentCraftingStation = null;

    // Movement + camera vars
    public Camera playerCamera;
    
    public bool canMove = true;


//     public delegate void PlayerStatChanged();
//     public static PlayerStatChanged playerStatChanged;


//     public void ButtonIncreasePlayerMaxHealth(float statChange)
//     {

//         playerData.maxHealth += statChange;
//         playerStatChanged?.Invoke();

//     }
//     public void ButtonDecreasePlayerMaxHealth(float statChange)
//     {

//         playerData.maxHealth -= statChange;
//         playerStatChanged?.Invoke();

//     }


    #region Function Calls


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


//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {

//         playerStatChanged?.Invoke();

//     }


    // Update is called once per frame
    private void Update()
    {

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            priceChangePower = 10;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            priceChangePower = 5;
        }
        else
        {
            priceChangePower = 1;
        }


        
    }

    #endregion

}
