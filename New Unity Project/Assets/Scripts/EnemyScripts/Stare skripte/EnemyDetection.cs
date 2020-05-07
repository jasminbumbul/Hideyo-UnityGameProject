using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float maxAngle; // ugao pod kojim te moze detektovati
    private Transform target; // main tenk
    public float heightMultiplayer = 1.5f;
    [SerializeField]
    public float lookRadius = 10f;
    public static bool isDetected = false; // glavna varijabla, da li je detektovan ili nije
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform; // postavimo target da je main tenk
        // isInFov=true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); // ovo smo prebacili iz skripte nemy atack

        Vector3 favline1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * lookRadius;
        Vector3 favline2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * lookRadius;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, favline1);
        Gizmos.DrawRay(transform.position, favline2);


        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * lookRadius);

    }

    public static bool inFOV(Transform CheckingObject, Transform player, float maxAngle, float maxRadious)
    {
        Vector3 directionBetween = (player.position - CheckingObject.position).normalized;
        directionBetween.y *= 0;

        float angle = Vector3.Angle(CheckingObject.forward, directionBetween);
        if (angle < maxAngle)
        {
            Ray ray = new Ray(CheckingObject.position, player.position - CheckingObject.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRadious))
            {
                if (hit.transform == player)
                    return true;
            }
        }



        return false;
    }
    // funckija koja vrsi detekciju  -> https://www.youtube.com/watch?v=BJPSiWNZVow, 
    //ovaj tutorial sam pratio, moro sam malo modifikovat kod jer je izbacivalo neki error,
    // varijablu heightMultiplayer sam pronaso u komentaru, neki link savjetovo 
    private void Update()
    {

        isDetected = inFOV(transform, target, maxAngle, lookRadius);
      
    }
}
