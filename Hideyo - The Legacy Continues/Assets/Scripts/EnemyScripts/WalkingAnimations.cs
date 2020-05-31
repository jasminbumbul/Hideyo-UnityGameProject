using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimations : MonoBehaviour
{
    Vector3 lastPosition;
    Transform myTransform;
    Animator animator;

    void Start()
    {
        myTransform = transform;
        lastPosition = myTransform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (myTransform.position != lastPosition)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);

        lastPosition = myTransform.position;
    }

}
