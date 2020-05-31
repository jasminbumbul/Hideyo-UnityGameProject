using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_moving_around : NPConectedPatrol
{

    public override void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
        // Returns if no points have been set up
        points[0].position = new Vector3(x, y, z - 8);
        points[1].position = new Vector3(x + 12, y, z);
        points[2].position = new Vector3(x, y, z + 12);
        //points[3].position = new Vector3(x + 3, y, z + 3);

        // Set the agent to go to the currently selected destination.

        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.

        destPoint = Random.Range(0, points.Length);
    }


}


