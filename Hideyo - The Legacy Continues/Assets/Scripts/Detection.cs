using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private Animator PlayerAnimator;
    public AudioSource swordHurtAudioSource;
    public AudioSource fistHurtAudioSource;

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
                //player udaren
                if (other.transform.gameObject.transform.root.name != "HumanModel")
                {
                    //ako se brani
                    if (PlayerAnimator.GetBool("IsDefending"))
                    {
                        this.gameObject.GetComponent<Health>().health -= 3;
                        swordHurtAudioSource.Play();
                    }
                    else //ako se ne brani
                    {
                        this.gameObject.GetComponent<Health>().health -= 10;
                        swordHurtAudioSource.Play();
                    }
                }
            }
            else
            {
                //player udara macem
                if (this.gameObject.tag == "Enemy")
                {
                    if (other.transform.gameObject.transform.root.tag != "Enemy")
                    {
                        Debug.Log("udar macem");
                        this.gameObject.GetComponent<Health>().health -= 30;
                        swordHurtAudioSource.Play();
                    }
                }
            }
        }
        //player udara bladeom
        if (other.transform.gameObject.name == "Blade")
        {
            if (this.gameObject.name == "HumanModel")
            {
                if (other.transform.gameObject.transform.root.name != "HumanModel")
                {
                    Debug.Log("udar bladeom");
                    this.gameObject.GetComponent<Health>().health -= 15;
                    fistHurtAudioSource.Play();
                }
            }
            else
            {
                        Debug.Log("ne znam");
                this.gameObject.GetComponent<Health>().health -= 30;
                fistHurtAudioSource.Play();
            }
        }

        //player udara sakom
        if (other.transform.gameObject.name == "FistHitTrigger")
        {
            if (this.gameObject.name == "HumanModel")
            {
                if (other.transform.gameObject.transform.root.name != "HumanModel")
                {
                        Debug.Log("udar sakom");
                    this.gameObject.GetComponent<Health>().health -= 5;
                    fistHurtAudioSource.Play();

                }
            }
            // else
            // {
            //     if (this.gameObject.tag == "Enemy")
            //     {
            //         if (other.transform.gameObject.transform.root.tag != "Enemy")
            //         {
            //             Debug.Log("ne znam 2");
            //             this.gameObject.GetComponent<Health>().health -= 10;
            //             fistHurtAudioSource.Play();

            //         }
            //     }
            // }
        }
    }

}
