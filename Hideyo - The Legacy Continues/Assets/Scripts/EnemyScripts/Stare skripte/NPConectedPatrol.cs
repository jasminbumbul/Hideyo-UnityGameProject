//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class NPConectedPatrol : MonoBehaviour
//{
//    [SerializeField]
//    bool patrolWaiting;


//    [SerializeField]
//    float totalWaitTime=3f;

//    [SerializeField]
//    float switchProbability = 0.2f;



//    NavMeshAgent navMeshAgent;
//    ConectedWayPoint currentWayPoint;
//    ConectedWayPoint previousWayPoint;


//    bool traveling;
//    bool waiting;
//    float waitTimer;
//    int waypointVisited;
//    void Start()
//    {
//        navMeshAgent = this.GetComponent<NavMeshAgent>();
//        if (navMeshAgent == null)
//        {
//            Debug.Log("nie zakacen");
//        }
//        else
//        {
//            if (currentWayPoint==null)
//            {
//                GameObject[] allwayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
//               if(allwayPoints.Length>0)
//                {
//                    while (currentWayPoint==null)
//                    {
//                        int random = UnityEngine.Random.Range(0, allwayPoints.Length);
//                        ConectedWayPoint startingWaypoint = allwayPoints[random].GetComponent<ConectedWayPoint>();

//                        if(startingWaypoint!=null)
//                        {
//                            currentWayPoint = startingWaypoint;
//                        }


//                    }
//                }
//                else
//                {
//                    Debug.Log("else");
//                }
//            }

//        setDestination();
//        }
//    }


//    // Update is called once per frame
//    void Update()
//    {
//        if (traveling && navMeshAgent.remainingDistance <= 1.0f)
//        {
//            traveling = false;
//            waypointVisited++;
//            if (patrolWaiting)
//            {
//                waiting = true;
//                waitTimer = 0;


//            }
//            else
//            {

//                setDestination();

//            }
//        }
//        if (waiting)
//        {
//            waitTimer += Time.deltaTime;
//            if (waitTimer >= totalWaitTime)
//            {
//                waiting = false;

//                setDestination();
//            }
//        }
//    }

//    private void setDestination()
//    {
//        if (waypointVisited > 0)
//        {
//            ConectedWayPoint nextWaypoimt = currentWayPoint.nextWayPoint(previousWayPoint);
//            previousWayPoint = currentWayPoint;
//            currentWayPoint = nextWaypoimt;

//        }
//            Vector3 targetVector = currentWayPoint.transform.position;
//            navMeshAgent.SetDestination(targetVector);
//            traveling = true;
//    }

//}
//Vector3 waypoint = new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z + 10);
//Vector3 waypoint1 = new Vector3(this.transform.position.x - 10, this.transform.position.y, this.transform.position.z + 10);
//Vector3 waypoint2 = new Vector3(this.transform.position.x + 5, this.transform.position.y, this.transform.position.z + 5);

using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class NPConectedPatrol : MonoBehaviour
{
    [SerializeField]
    bool patrolWaiting;


    [SerializeField]
    float totalWaitTime = 3f;


    float waitTimer;

    protected float x;
    protected float y;
    protected float z;
    public Transform[] points;
    protected int destPoint = 0;
    protected NavMeshAgent agent;




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        x = this.transform.position.x;
        y = this.transform.position.y;
        z = this.transform.position.z;




        agent.autoBraking = false;

        GotoNextPoint();
    }


    virtual public void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
        // Returns if no points have been set up
        points[0].position = new Vector3(x, y, z);
        points[1].position = new Vector3(x, y, z);
        points[2].position = new Vector3(x, y, z);

        // Set the agent to go to the currently selected destination.
        var temp = destPoint;
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.

        destPoint = Random.Range(0, points.Length);



    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (this.transform.gameObject != null)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {


                //GotoNextPoint();
                if (patrolWaiting)
                {
                    waitTimer += Time.deltaTime;
                    agent.SetDestination(agent.transform.position);
                    if (waitTimer >= totalWaitTime)
                    {
                        GotoNextPoint();
                        waitTimer = 0;
                    }
                }
                else
                {
                    GotoNextPoint();
                }

            }

        }
    }

}