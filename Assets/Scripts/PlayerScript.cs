using System;
using Unity.Mathematics;
using UnityEngine;

// Will create one if we don't have one
[RequireComponent(typeof(CharacterController))]

public class PlayerScript : MonoBehaviour
{
    CharacterController characterController;

    public PlayerData playerData;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        characterController = GetComponent<CharacterController>();
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



        playerData.playerPos = transform.position;
    }
}
