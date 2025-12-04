using UnityEngine;

// Will create one if we don't have one
[RequireComponent(typeof(CharacterController))]

public class PlayerScript : MonoBehaviour
{

    // Player stuff

    public PlayerData playerData;

    public int priceChangePower = 1;


    public RaycastHit containerHit;
    public RaycastHit priceChangeHit;
    public RaycastHit vendorHit;
    public RaycastHit craftingHit;


    public GameObject currentCraftingStation = null;

    // Movement + camera vars
    public Camera playerCamera;
    
    public bool canMove = true;


    public delegate void PlayerStatChanged();
    public static PlayerStatChanged playerStatChanged;


    public static PlayerScript instance { get; private set;  }


    public void ButtonIncreasePlayerMaxHealth(float statChange)
    {

        playerData.maxHealth += statChange;
        playerStatChanged?.Invoke();

    }
    public void ButtonDecreasePlayerMaxHealth(float statChange)
    {

        playerData.maxHealth -= statChange;
        playerStatChanged?.Invoke();

    }


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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerStatChanged?.Invoke();

    }


    // Update is called once per frame
    private void Update()
    {


        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
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
