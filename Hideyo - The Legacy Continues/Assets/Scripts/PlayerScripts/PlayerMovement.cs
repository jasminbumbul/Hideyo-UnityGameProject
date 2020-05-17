using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //brzine kretanja


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




    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GameObject.Find("HumanModel").GetComponent<Animator>();
    }

   
    private void Update()
    {


        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundRadius, GroundMask);

        if (IsGrounded && Velocity.y<0)
        {
            Velocity.y = -2f;
        }


       



        //razlicite brzine za razlicite smjerove kretanja
        //if (!Animator.GetBool("NotMoving"))
        //{

        

        //skok
        if (Input.GetButton("Jump") && IsGrounded)
        {
            Animator.SetBool("JumpTrigger",true);
            Velocity.y = Mathf.Sqrt(JumpHeight*-2f * Gravity);
        }
        else
        {
            Animator.SetBool("JumpTrigger", false);
        }


        //primjena gravitacije
        Velocity.y += Gravity * Time.deltaTime;
        Velocity.y += Gravity * Time.deltaTime;
        CharacterController.Move(Velocity* Time.deltaTime);

      
    }

      
}
