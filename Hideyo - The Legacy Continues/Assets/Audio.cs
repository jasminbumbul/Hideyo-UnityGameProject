using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private CharacterController cc;
    private Animator animator;
    public AudioSource walkingAudioSource;
    public AudioSource jumpingAudioSource;
    public AudioSource landingAudioSource;
    bool soundHasPlayed = false;

    void Start()
    {
        cc=GetComponent<CharacterController>();
        animator=GetComponent<Animator>();
    }


    void Update()
    {
        // if(cc.isGrounded && cc.velocity.magnitude>0f && !audioSource.isPlaying)
        if(cc.isGrounded && !walkingAudioSource.isPlaying && animator.GetBool("IsWalking"))
        {
            walkingAudioSource.volume=Random.Range(0.05f,0.1f);
            walkingAudioSource.pitch=Random.Range(0.8f,1.1f);
            walkingAudioSource.Play();
            soundHasPlayed=false;
        }
        if(!cc.isGrounded && !soundHasPlayed)
        {
            jumpingAudioSource.Play();
            soundHasPlayed=true;
            Invoke("landSound",0.6f);
        }
    }
    private void landSound(){
        if(soundHasPlayed)
        {
            landingAudioSource.Play();
            soundHasPlayed=false;
        }
    }
}
