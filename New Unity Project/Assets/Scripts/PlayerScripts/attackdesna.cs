using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackdesna : MonoBehaviour
{
    Animator animator;
    Transform temp;
    public static bool jeOdradio;

    void Start()
    {
        animator = GetComponent<Animator>();
        jeOdradio = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) )
        {
            if (animator.GetBool("SwordIsOut"))
            {
                animator.SetBool("SwordIsOut", false);
                Invoke("removeParent", 0.95f);
            }
            else
            {
                animator.SetBool("SwordIsOut", true);
                Invoke("makeParent", 1f);
            }
        }

        if (Input.GetMouseButtonDown(0) && animator.GetBool("SwordIsOut"))
        {
            animator.SetBool("SwordAttack", true);
        }
        if (animator.GetBool("SwordAttack"))
        {
            Invoke("setFalse", 0.5f);
        }
        
        if (Input.GetMouseButton(1) && animator.GetBool("SwordIsOut"))
        {
            animator.SetBool("Protect", true);
        }
        else
        {
            animator.SetBool("Protect", false);
        }
        if ((Input.GetMouseButtonDown(0) && !animator.GetBool("SwordIsOut")) && jeOdradio == false )
        {
            Debug.Log(jeOdradio);
            animator.SetBool("Bokser", true);
            jeOdradio = true;
        }
        if (animator.GetBool("Bokser"))
        {
            Invoke("setFalse2", 0.5f);
        }

    }

    private void makeParent()
    {
        temp = GameObject.Find("katana").transform.parent;
        GameObject.Find("katana").transform.parent = this.transform;
    }
    private void removeParent()
    {
        GameObject.Find("katana").transform.parent = temp.transform;
    }
    private void setFalse()
    {
        animator.SetBool("SwordAttack", false);
    }
    private void setFalse2()
    {
        animator.SetBool("Bokser", false);
    }
}
