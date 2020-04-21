using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_atack : MonoBehaviour
{
    [SerializeField]
    Transform destination;
    Transform target;
    NavMeshAgent navMashAgent;
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
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            navMashAgent.SetDestination(target.position);
            FireProjectile();
        }



        if(distance<=navMashAgent.stoppingDistance)
        {
            FaceTrget();
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
