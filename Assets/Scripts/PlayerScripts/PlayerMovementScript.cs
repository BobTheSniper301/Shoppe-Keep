// using UnityEngine;

// public class PlayerMovementScript : MonoBehaviour
// {
//     [SerializeField] CharacterController characterController;

//     bool canMove;

//     private float walkSpeed = 6f;
//     private float runSpeed = 8f;
//     private float jumpPower = 5f;
//     private float gravity = 10f;

//     [SerializeField] float lookSpeed = 10f;
//     [SerializeField] float lookXLimit = 89f;

//     Vector3 moveDirection = Vector3.zero;
//     float rotationX = 0;

//     void FixedUpdate()
//     {
//         if (PlayerScript.instance != null)
//         {
//             canMove = PlayerScript.instance.canMove;
//         }

//             #region Handles Movement
//             Vector3 forward = transform.TransformDirection(Vector3.forward);
//         Vector3 right = transform.TransformDirection(Vector3.right);

//         // Press "Left_Shift" to run
//         bool isRunning = Input.GetKey(KeyCode.LeftShift);
//         float curSpeedX = PlayerScript.instance.canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
//         float curSpeedY = PlayerScript.instance.canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
//         float movementDirectionY = moveDirection.y;
//         moveDirection = (forward * curSpeedX) + (right * curSpeedY);

//         #endregion


//         #region Handles Jumping
//         if (Input.GetButton("Jump") && PlayerScript.instance.canMove && characterController.isGrounded)
//         {
//             Debug.Log("jump");
//             moveDirection.y = jumpPower;
//         }
//         else
//         {
//             moveDirection.y = movementDirectionY;
//         }

//         if (!characterController.isGrounded)
//         {
//             moveDirection.y -= gravity * Time.deltaTime;
//         }

//         #endregion


//         #region Handles Rotation
//         characterController.Move(moveDirection * Time.deltaTime);

//         if (PlayerScript.instance.canMove)
//         {
//             rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
//             rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
//             PlayerScript.instance.playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
//             transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
//         }

//         #endregion




//         PlayerScript.instance.playerData.playerPos = transform.position;
//     }


// }
