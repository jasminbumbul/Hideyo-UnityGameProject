using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //brzine kretanja
    public float ForwardSpeed = 7f;
    public float BackwardSpeed = 3f;
    public float SideSpeed = 3f;

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

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 Movement = transform.right * x + transform.forward * z;

        //razlicite brzine za razlicite smjerove kretanja
        if (z > 0)
        {
            CharacterController.Move(Movement * ForwardSpeed * Time.deltaTime);
            Animator.SetBool("IsWalking", true);
        }
        if (z<0)
        {
            CharacterController.Move(Movement * BackwardSpeed * Time.deltaTime);
            Animator.SetBool("IsWalking", true);
        }
        if (z==0 && (x<0 || x>0))
        {
            CharacterController.Move(Movement * SideSpeed * Time.deltaTime);
            Animator.SetBool("IsWalking", true);
        }
        if (x==0 && z==0)
        {
            Animator.SetBool("IsWalking", false);
        }

        //brzo trcanje na leftShift
        if (Input.GetAxis("FasterWalking")>0 && Animator.GetBool("IsWalking"))
        {
            Animator.SetBool("IsRunning", true);
            ForwardSpeed =15f;
        }
        else 
        {
            Animator.SetBool("IsRunning", false);
            ForwardSpeed = 7f;
        }

        //skok
        if (Input.GetButton("Jump") && IsGrounded)
        {
            Velocity.y = Mathf.Sqrt(JumpHeight*-2f * Gravity);
            Animator.SetBool("IsJumping", true);
        }
        else
        {
            Animator.SetBool("IsJumping", false);
        }

        //primjena gravitacije
        Velocity.y += Gravity * Time.deltaTime;
        Velocity.y += Gravity * Time.deltaTime;
        CharacterController.Move(Velocity* Time.deltaTime);

      
    }
}
