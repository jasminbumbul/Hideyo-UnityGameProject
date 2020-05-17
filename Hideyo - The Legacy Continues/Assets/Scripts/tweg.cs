using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tweg : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    NavMeshAgent agent;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        if(agent.remainingDistance<=1)
        {
            Debug.Log("fight");
        }
    }
}
