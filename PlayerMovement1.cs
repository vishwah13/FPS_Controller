using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement1 : MonoBehaviour
{
    private CharacterController controller;
    
    public float gravity = -9.82f;
    public float jumpHeight=3f;
    public float walkSpeed = 12f;
    public float runningSpeed;
    private float currentSpeed;
    private Vector3 velocity;
    
    //for checking ground 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    private bool isCrouching = false;
    
    //for sliding
    public float slidingSpeed=6f;
    private bool isSliding;
    private float slideTimer;
    private float slideTimeMax = 1f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isCrouching = true;
        }
        //for movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        //for running 
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ;
        currentSpeed = isRunning ? runningSpeed : walkSpeed;
        

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //to create gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        //for crouching
        if (!isGrounded)
        {
            isCrouching = false;
        }
        if (Input.GetKeyDown(KeyCode.C) && isCrouching)
        {
            controller.height = .5f;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            controller.height = 2f;
        }
        
        //for sliding
        if (isRunning && Input.GetKey(KeyCode.LeftControl) && !isSliding)
        {
            slideTimer = 0.0f;
            isSliding = true;
        }

        if (isSliding)
        {
            currentSpeed = slidingSpeed;
            controller.height = 0.5f;

            slideTimer += Time.deltaTime;
            if (slideTimer > slideTimeMax)
            {
                isSliding = false;

            }
        }

        if (!isSliding && !isCrouching)
        {
            controller.height = 2f;
        }
        
    }
}
