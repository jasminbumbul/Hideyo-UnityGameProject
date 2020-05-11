using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //brzine kretanja
    public float ForwardSpeed = 0.0f;
    public float BackwardSpeed = 0.0f;
    public float SideSpeed = 0.0f;

    private CharacterController CharacterController;





    //za skok
    Vector3 Velocity;
    public float JumpHeight = 3f;
    private  float Gravity = -9.81f;
    public Transform GroundCheck;
    public float GroundRadius = 0.04f;
    public LayerMask GroundMask;
    private bool IsGrounded;

    private Animator Animator;

    private float RotX = 0.0f;
    private float RotY = 0.0f;
    public float MouseX;
    public float MouseY;
    public float InputSensitivity = 150.0f;


    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GameObject.Find("HumanModel").GetComponent<Animator>();
    }

   
    private void Update()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        RotY += MouseX * InputSensitivity * Time.deltaTime;
        RotX -= MouseY * InputSensitivity * Time.deltaTime;

        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundRadius, GroundMask);

        if (IsGrounded && Velocity.y<0)
        {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 Movement;

        if (x==0 && z==0)
        {
            Movement = Vector3.zero;

        }
        else
        {
            Movement = transform.right * x + transform.forward * z;

        }



        //razlicite brzine za razlicite smjerove kretanja
        //if (!Animator.GetBool("NotMoving"))
        //{

            if (z > 0)
            {
                this.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                CharacterController.Move(Movement * ForwardSpeed * Time.deltaTime);
                Animator.SetBool("IsWalking", true);
            }
            if (z < 0)
            {
                this.transform.localScale = new Vector3(0.7f, 0.7f, -0.7f);
                CharacterController.Move(Movement * BackwardSpeed * Time.deltaTime);
                Animator.SetBool("IsWalking", true);
            }

            if (z == 0 && (x < 0 || x > 0))
            {
                if (x > 0)
                {
                    Animator.SetBool("IsWalkingSideRight", true);

                }

                if (x < 0)
                {
                    Animator.SetBool("IsWalkingSideLeft", true);

                }


                //CharacterController.Move(Movement * SideSpeed * Time.deltaTime);
            }
            else
            {
                Animator.SetBool("IsWalkingSideRight", false);
                Animator.SetBool("IsWalkingSideLeft", false);

            }


        if (x==0 && z==0)
        {
            Animator.SetBool("IsWalking", false);
        }


        //punch
        if (Input.GetMouseButton(0) && !Animator.GetBool("SwordOut") && Animator.GetBool("NotMoving"))
        {
            Animator.SetBool("IsPunching", true);
        }
        else
        {
            Animator.SetBool("IsPunching", false);

        }


        //brzo trcanje na leftShift
        if (Input.GetAxis("FasterWalking")>0 && Animator.GetBool("IsWalking"))
        {
            Animator.SetBool("IsRunning", true);
            ForwardSpeed =8f;
        }
        else 
        {
            Animator.SetBool("IsRunning", false);
            ForwardSpeed = 4f;
        }

        //skok
        if (Input.GetButton("Jump") && IsGrounded)
        {
            Animator.SetBool("IsJumping",true);
            Velocity.y = Mathf.Sqrt(JumpHeight*-2f * Gravity);
        }
        else
        {
            Animator.SetBool("IsJumping",false);
        }
 

        //primjena gravitacije
        Velocity.y += Gravity * Time.deltaTime;
        Velocity.y += Gravity * Time.deltaTime;
        CharacterController.Move(Velocity* Time.deltaTime);

      
    }

      
}
