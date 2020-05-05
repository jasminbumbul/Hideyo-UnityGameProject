using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackLijeva : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && attackdesna.jeOdradio)
        {
            Debug.Log(attackdesna.jeOdradio);
            animator.SetBool("Bokser", true);
            attackdesna.jeOdradio = false;
        }
        if (animator.GetBool("Bokser"))
        {
            Invoke("setFalse2", 0.8f);
        }
    }

    private void setFalse2()
    {
        animator.SetBool("Bokser", false);
    }
}
