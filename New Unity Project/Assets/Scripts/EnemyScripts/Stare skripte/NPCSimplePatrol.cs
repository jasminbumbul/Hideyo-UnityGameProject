using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSimplePatrol : MonoBehaviour
{
    [SerializeField]
    bool patrolWaiting;


    [SerializeField]
    float totalWaitTime;

    [SerializeField]
    float switchProbability=0.2f;

    [SerializeField]
    List<Waypoint> patrolPoints;

    NavMeshAgent navMeshAgent;
    int currentPatrolIndeks;
    bool traveling;
    bool waiting;
    bool patrolForward;
    float waitTimer;
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(navMeshAgent==null)
        {
            Debug.Log("nie zakacen");
        }
        else
        {
            if(patrolPoints !=null && patrolPoints.Count>=2)
            {
                currentPatrolIndeks = 0;
                setDestination();
            }
            else
            {
                Debug.Log("greska");
            }
        }
    }

 
    // Update is called once per frame
    void Update()
    {
        if(traveling && navMeshAgent.remainingDistance <=1.0f)
        {
            traveling = false;
            if(patrolWaiting)
            {
                waiting = true;
                waitTimer = 0;
            }
            else
            {
                chagePatrolPoint();
                setDestination();
            }
        }
        if(waiting)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= totalWaitTime)
            {
                waiting = false;
                chagePatrolPoint();
                setDestination();
            }
        }
    }

    private void chagePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f,1f)<switchProbability)
        {
            patrolForward = !patrolForward;
        }
        if(patrolForward)
        {
            currentPatrolIndeks = (currentPatrolIndeks - 1) % patrolPoints.Count;

        }
        else
        {
            if(--currentPatrolIndeks<0)
            {
                currentPatrolIndeks=patrolPoints.Count-1;
               
            }
        }
    }

    private void setDestination()
    {
        if(patrolPoints!=null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndeks].transform.position;
            navMeshAgent.SetDestination(targetVector);
            traveling = true; 
        }
    }

}
