using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public Rigidbody playerRigid;
    public float walkSpeed, sprintMultiplier, jumpPower, gravity, aimSpeed, aimLimit, acceleration;
    public KeyCode left, right, forward, backward, sprint, jump;
    public GameObject gun;

    private Vector3 accelerationVector = Vector3.zero;
    private float forwardSpeed, backwardSpeed, leftSpeed, rightSpeed, rotationX;
    private bool isSprinting, isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        walkSpeed = 5f;
        sprintMultiplier = 1.75f;
        jumpPower = 12f;
        gravity = 3f;
        rotationX = 0f;
        acceleration = 15f;
        left = KeyCode.A;
        right = KeyCode.D;
        forward = KeyCode.W;
        backward = KeyCode.S;
        jump = KeyCode.Space;
        sprint = KeyCode.LeftShift;
        aimSpeed = 1f;
        aimLimit = 85f;
    }

    void Update()
    {
        #region Movement
        if(Input.GetKeyDown(forward) )
        {
            forwardSpeed = 1;
        }
        if(Input.GetKeyUp(forward))
        {
            forwardSpeed = 0;
        }
        if(Input.GetKeyDown(backward))
        {
            backwardSpeed = -1;
        }
        if(Input.GetKeyUp(backward))
        {
            backwardSpeed = 0;
        }
        if(Input.GetKeyDown(left))
        {
            leftSpeed = -1;
        }
        if(Input.GetKeyUp(left))
        {
            leftSpeed = 0;
        }
        if(Input.GetKeyDown(right))
        {
            rightSpeed = 1;
        }
        if(Input.GetKeyUp(right))
        {
            rightSpeed = 0;
        }

        //where the fixed update stuff came from
        accelerationVector.z = forwardSpeed + backwardSpeed;
        accelerationVector.x = leftSpeed + rightSpeed;
        accelerationVector = Vector3.ClampMagnitude(accelerationVector, 1) * acceleration;
        
        if(playerRigid.velocity.magnitude < walkSpeed && isGrounded) // INCLUDES JUMPING, FIX THIS TO BE JUST XZ AXIS
        {
            playerRigid.AddRelativeForce(accelerationVector, ForceMode.Acceleration);
        }
        playerRigid.AddForce(new Vector3(0, -1.0f, 0) * playerRigid.mass * gravity, ForceMode.Acceleration);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, new Vector3(0, -.2f, 0), out hit, 1))
        {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
        Debug.DrawRay(transform.position, new Vector3(0, -0.2f, 0), Color.red);

        if(Input.GetKeyDown(jump) && isGrounded)
        {
            playerRigid.AddForce(new Vector3(0, 1.0f, 0) * jumpPower, ForceMode.Impulse);
        }
        #endregion

        #region Aim
        rotationX += -Input.GetAxis("Mouse Y") * aimSpeed;
        rotationX = Mathf.Clamp(rotationX, -aimLimit, aimLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * aimSpeed, 0);
        #endregion

        #region Shoot
        if(Input.GetMouseButtonDown(0))
        {
            gun.GetComponent<GunController>().shoot();
        }
        #endregion
    }

    void FixedUpdate() {
        // Time.fixedDeltaTime = 1/10000;

    }
}
