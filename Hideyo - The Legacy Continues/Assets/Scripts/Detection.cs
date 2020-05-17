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
                if (other.transform.gameObject.transform.root.name != "HumanModel")
                {
                    if (PlayerAnimator.GetBool("IsDefending"))
                    {
                        this.gameObject.GetComponent<Health>().health -= 3;
                        swordHurtAudioSource.Play();
                    }
                    else
                    {
                        this.gameObject.GetComponent<Health>().health -= 10;
                        swordHurtAudioSource.Play();
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
                        swordHurtAudioSource.Play();
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
                    fistHurtAudioSource.Play();
                }
            }
            else
            {
                this.gameObject.GetComponent<Health>().health -= 30;
                fistHurtAudioSource.Play();
            }
        }

        if (other.transform.gameObject.name == "FistHitTrigger")
        {
            if (this.gameObject.name == "HumanModel")
            {
                if (other.transform.gameObject.transform.root.name != "HumanModel")
                {
                    this.gameObject.GetComponent<Health>().health -= 5;
                    fistHurtAudioSource.Play();

                }
            }
            else
            {
                if (this.gameObject.tag == "Enemy")
                {
                    if (other.transform.gameObject.transform.root.tag != "Enemy")
                    {
                        this.gameObject.GetComponent<Health>().health -= 10;
                        fistHurtAudioSource.Play();

                    }
                }
            }
        }
    }

}
