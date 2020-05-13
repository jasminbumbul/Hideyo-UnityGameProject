using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private Animator PlayerAnimator;
    void Start()
    {
        PlayerAnimator = GameObject.Find("HumanModel").GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.name == "Sphere")
        {
            if (this.gameObject.name == "HumanModel")
            {
                if (other.transform.gameObject.transform.root.name != "HumanModel")
                {
                    if (PlayerAnimator.GetBool("IsDefending"))
                    {
                        this.gameObject.GetComponent<Health>().health -= 3;
                        Debug.Log("-5");
                    }
                    else
                    {
                        this.gameObject.GetComponent<Health>().health -= 10;
                        Debug.Log("-10");

                    }
                }
            }
            else
            {
                if (this.gameObject.tag == "Enemy")
                {
                    if (other.transform.gameObject.transform.root.tag != "Enemy")
                    {
                        this.gameObject.GetComponent<Health>().health -= 30;
                    }
                }
            }
        }
        if (other.transform.gameObject.name == "Blade")
        {
            if (this.gameObject.name == "HumanModel")
            {
                if (other.transform.gameObject.transform.root.name != "HumanModel")
                {

                    this.gameObject.GetComponent<Health>().health -= 15;



                }
            }
            else
            {
                this.gameObject.GetComponent<Health>().health -= 30;
                Debug.Log(this.gameObject.name);
            }
        }
    }

}
