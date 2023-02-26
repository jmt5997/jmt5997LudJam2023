using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public float crouchHeight, normalHeight;
    public float crouchSpeed = 2f;

    public Rigidbody paintball;
    public float paintballSpeed = 1f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    public CharacterController characterController;
    public GameObject gunBarrel;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        #endregion
        #region Jumping
        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if(!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion
        #region Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if(canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion
        #region Crouch
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
           characterController.height = crouchHeight;
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            characterController.height = normalHeight;
        }
        #endregion
        #region Interact
        if(Input.GetKey(KeyCode.E))
        {
            
        }
        #endregion
        #region Shoot
        if(Input.GetMouseButtonDown(0))
        {
            Rigidbody clone;
            clone = Instantiate(paintball, gunBarrel.transform.position, Quaternion.identity);
            clone.velocity = transform.TransformDirection(Vector3.forward * paintballSpeed);
            // paintball.velocity = transform.TransformDirection(Vector3(0, 0, paintballSpeed));
        }
        #endregion
    }
}
