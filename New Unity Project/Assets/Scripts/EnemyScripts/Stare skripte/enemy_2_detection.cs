using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_2_detection : MonoBehaviour
{
    public Transform player;
    public static bool detekcija = false;
    public enemy_moving_around skripa;
    NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        skripa = this.GetComponent<enemy_moving_around>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void isDetected()
    {

        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 20 && angle < 90)
        {
            
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            if (direction.magnitude > 1)
            {
               // navMeshAgent.speed = 6;
                navMeshAgent.destination = player.position;
               

                
            }
            else
            {
                Debug.Log("fight");
            }

        }

    }
    void Update()
    {
        isDetected();

    }
}
