using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class ConectedWayPoint : Waypoint
    {
        [SerializeField]
        protected float connectivityRadius = 50f;

        List<ConectedWayPoint> connections;
        public void Start()
        {
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            connections = new List<ConectedWayPoint>();
            Debug.Log("desava se nesto");
            for (int i = 0; i < allWaypoints.Length; i++)
            {
                ConectedWayPoint nextWayPoint = allWaypoints[i].GetComponent<ConectedWayPoint>();
                if (nextWayPoint != null)
                {
                    if (Vector3.Distance(this.transform.position, nextWayPoint.transform.position) <= connectivityRadius && nextWayPoint != this)
                    {
                        connections.Add(nextWayPoint);
                    }
                }
            }
        }
        public ConectedWayPoint nextWayPoint(ConectedWayPoint previousWayPoint)
        {
            if (connections.Count == 0)
            {
                return null;
            }
            else if (connections.Count == 1 && connections.Contains(previousWayPoint))
            {
                return previousWayPoint;
            }
            else
            {
                ConectedWayPoint nextWaypoint;
                int nextIndex = 0;
                do
                {
                    nextIndex = UnityEngine.Random.Range(0, connections.Count);
                    nextWaypoint = connections[nextIndex];
                } while (nextWaypoint == previousWayPoint);
                Debug.Log(nextWaypoint);
                return nextWaypoint;
            }
        }

    }

