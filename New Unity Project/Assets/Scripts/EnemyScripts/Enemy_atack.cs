using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_atack : MonoBehaviour
{
    [SerializeField]
    private Transform destination;
    private Transform target;
    private NavMeshAgent navMashAgent;
    public float lookRadius = 10f;

    //projectile settings
    public GameObject Projectile;
    public Transform EnemyProjectileContainer;
    public Transform ProjectileSpawnPoint;

    [Tooltip("Projectile fire rate")]
    [Range(0, 1)]
    public float FireRate;
    private float NextFire = 0.0f;
    //

    void Start()
    {
        navMashAgent = this.GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        navMashAgent.SetDestination(destination.transform.position);
        
    }

   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    void Update()
    {
        float TargetDistance = Vector3.Distance(target.position, transform.position);
        float DestinationDistance = Vector3.Distance(destination.position, transform.position);
      
        if (TargetDistance <= lookRadius)
        {
            navMashAgent.SetDestination(target.position);
            FaceTrget();
            FireProjectile();
        }
        else
        {
            navMashAgent.SetDestination(destination.position);
        }
        //Debug.Log(("TargetDistance --> " + TargetDistance));
        //Debug.Log((" DestinationDistance --> " + DestinationDistance));

        //treba poboljsati
        if (DestinationDistance < 13)
        {
            FireProjectile();

        }
    }

    private void FireProjectile()
    {
        if (Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            GameObject.Instantiate(Projectile, ProjectileSpawnPoint.position, Projectile.transform.rotation, EnemyProjectileContainer);
        }
    }

    private void FaceTrget()
    {
       

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

      
    }
}
