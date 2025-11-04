using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// Will create one if we don't have one
[RequireComponent(typeof(CharacterController))]

public class PlayerScript : MonoBehaviour
{

    // Scene pull stuff
    public UiManager uiManager;

    public GameControllerScript gameControllerScript;

    // Player stuff
    CharacterController characterController;

    public PlayerData playerData;

    public int priceChangePower = 1;

    // Raycast Player Look
    LayerMask playerLookMask;
    LayerMask playerLookPriceMask;

    public RaycastHit containerHit;
    public RaycastHit priceChangeHit;

    [SerializeField] float PlayerLookDistance;


    // Movement + camera vars
    public Camera playerCamera;
    private Ray cameraRay;
    private float walkSpeed = 6f;
    private float runSpeed = 8f;
    private float jumpPower = 5f;
    private float gravity = 10f;

    [SerializeField] float lookSpeed = 10f;
    [SerializeField] float lookXLimit = 89f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;


    public delegate void PlayerStatChanged();
    public static PlayerStatChanged playerStatChanged;


    // Checks if looking at an item container
    void ItemContainerCheck()
    {

        if (Physics.Raycast(cameraRay, out containerHit, PlayerLookDistance, playerLookMask) && !uiManager.inMenu && ! uiManager.canChangePrice)
        {

            uiManager.ContainerText(true);

        }
        else
        {

            uiManager.ContainerText(false);

        }

    }

    void ItemPriceChangeLook()
    {
        if (Physics.Raycast(cameraRay, out priceChangeHit, PlayerLookDistance, playerLookPriceMask))
        {

            PedestalScript pedestal = priceChangeHit.transform.parent.root.GetComponent<PedestalScript>();
            
            uiManager.canChangePrice = true;
            // Debug.Log(priceChangeHit.transform.name);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Debug.Log("Mouse down!");
                pedestal.PedestalChange(priceChangeHit.transform.gameObject.name, priceChangePower);
            }

        }
        else
        {

            uiManager.canChangePrice = false;
        }

        // Debug.Log("canchangeprice: " + uiManager.canChangePrice);
        
    }


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

    void Awake()
    {

        characterController = GetComponent<CharacterController>();

        playerLookMask = LayerMask.GetMask("PlayerLookContainer");

        playerLookPriceMask = LayerMask.GetMask("PlayerLookPrice");

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        #region Handles Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press "Left_Shift" to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion


        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion


        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion

        cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            priceChangePower = 5;
        }
        else
        {
            priceChangePower = 1;
        }


        ItemContainerCheck();

        ItemPriceChangeLook();




        // Debug.DrawRay(cameraRay.origin, cameraRay.direction * PlayerLookDistance, Color.red, 0.5f);


        playerData.playerPos = transform.position;
    }

    #endregion

}
