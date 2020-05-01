using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    Animator animator;
    Transform temp;

    void Start()
    {
        animator = GetComponent<Animator>();
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
}
