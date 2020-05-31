using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
   
    [SerializeField] private float movenetSpeed = 2f;
    private float currentSpeed = 0f;
    private float speedSmoothVelocity = 0f;
    private float speedSmoothTime = 0.1f;
    private float rotationSpeed = 0.1f;
    private float gravity = 3f;
    public float JumpHeight = 6f;
    public bool isDefending = false;


    private Transform mainCameraTransform = null;
    private CharacterController controller = null;
    private Animator animator = null;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCameraTransform = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;
        forward.Normalize();
        right.Normalize();

        forward.y = 0;
        right.y = 0;
        Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;
        Vector3 gravityVector = Vector3.zero;
        if (!controller.isGrounded)
        {
            gravityVector.y -= gravity;
        }
        if (desiredMoveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed*5f);

        }
        float targetSpeed = movenetSpeed * movementInput.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        controller.Move(desiredMoveDirection * currentSpeed * Time.deltaTime);
        controller.Move(gravityVector * Time.deltaTime);
        if (z != 0 || x != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);

        }
        if (Input.GetAxis("FasterWalking") > 0 && animator.GetBool("IsWalking"))
        {
            animator.SetBool("IsRunning", true);
            movenetSpeed = 8f;
        }
        else
        {
            animator.SetBool("IsRunning", false);
            movenetSpeed = 4f;
        }
        //if (controller.isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}
        //if (Input.GetButton("Jump") && controller.isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        //    animator.SetTrigger("JumpTrigger");
        //}
    //if(Input.GetKeyDown(KeyCode.Mouse1))
    //    {
    //        Debug.Log("odbrana");
    //        animator.SetBool("IsDefending", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("IsDefending", false);
    //    }
      
    }
    
}
