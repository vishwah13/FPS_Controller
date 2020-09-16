using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement1 : MonoBehaviour
{
    private CharacterController controller;
    private float x;
    private float z;
    
    public float gravity = -9.82f;
    public float jumpHeight=3f;
    public float walkSpeed = 12f;
    public float runningSpeed;
    private float currentSpeed;
    private Vector3 velocity;
    public Vector3 move;
    
    //for checking ground 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    
    //for crouching
    private bool isCrouching;
    public float crouchSpeed= 4f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        GetInput();
    }

    private void FixedUpdate()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isCrouching = true;
        }
        
        move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
        
        //for crouching
        if (!isGrounded)
        {
            isCrouching = false;
        }
        
        
        //to create gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void GetInput()
    {
        //for movement
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        
        
        //for checking if plaer is running or crouching and set the current speed
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ;
        bool crouch = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        currentSpeed = crouch ? crouchSpeed : isRunning ? runningSpeed : walkSpeed;
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {    //for add jump force to player 
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching)
        {
            startCrouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            stopCrouch();
        }
    }

    void startCrouch()
    {
        controller.height = .5f;
        
    }

    void stopCrouch()
    {
        controller.height = 2f;
    }
}
