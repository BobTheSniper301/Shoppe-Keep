using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

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


    // Raycast Player Look
    LayerMask playerLookMask;

    public RaycastHit containerHit;

    [SerializeField] float PlayerLook;

    #region movement + camera vars
    public Camera playerCamera;
    private float walkSpeed = 6f;
    private float runSpeed = 8f;
    private float jumpPower = 5f;
    private float gravity = 10f;

    [SerializeField] float lookSpeed  = 10f;
    [SerializeField] float lookXLimit = 89f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    #endregion


    void Awake()
    {

        characterController = GetComponent<CharacterController>();

        playerLookMask = LayerMask.GetMask("PlayerLook");
        

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


        if (Physics.Raycast(transform.position, GetComponentInChildren<Camera>().gameObject.transform.forward, out containerHit, PlayerLook, playerLookMask) && ! uiManager.inMenu)
        {
            // Debug.Log(hit.collider.gameObject.name);
            // Debug.Log("looking at object");
            uiManager.itemCanPlace = true;
            // Debug.Log(uiManager.itemCanPlace);
        }
        else
        {
            uiManager.itemCanPlace = false;
            // Debug.Log(uiManager.itemCanPlace);
        }


            playerData.playerPos = transform.position;
    }
}
