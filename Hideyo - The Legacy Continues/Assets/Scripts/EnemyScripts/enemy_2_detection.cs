using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_2_detection : MonoBehaviour
{
    public Transform player;
    public AudioSource SwordSlashAudioSource;
    public static bool detekcija = false;
    // public enemy_moving_around skripa;
    NavMeshAgent navMeshAgent;

    private Animator animator;
    float timer = 0.0f;

    void Start()
    {
        // skripa = this.GetComponent<enemy_moving_around>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    public void isDetected()
    {





        try
        {
            Vector3 direction = player.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            //float distance = Vector3.Distance(transform.position, player.transform.position);
            float x = transform.position.x - player.transform.position.x;
            float z = transform.position.z - player.transform.position.z;
            float y = transform.position.y - player.transform.position.y;


            if ((Vector3.Distance(transform.position, player.transform.position) < 20 && (transform.position.y - player.transform.position.y) < 1) && angle < 180)
            {
                if(this.name=="SATOSHI")
                {
                navMeshAgent.speed = 7;

                }
                else
                {
                    navMeshAgent.speed = 3;

                }
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                if (direction.magnitude >= 3)
                {
                    if (this.name == "SATOSHI")
                    {
                        navMeshAgent.speed = 7;

                    }
                    else
                    {
                        navMeshAgent.speed = 3;

                    }
                    navMeshAgent.destination = player.position;


                }
                else
                {
                    navMeshAgent.stoppingDistance = 3;

                    if (timer > 1.5)
                    {
                        animator.SetTrigger("IsAttacking");
                        SwordSlashAudioSource.Play();
                        timer = 0;
                    }
                }

            }





            navMeshAgent.stoppingDistance = 3;
        }
        catch (System.Exception)
        {


        }

    }
    void Update()
    {
        timer += Time.deltaTime;
        timer %= 60;
        isDetected();

    }

}
