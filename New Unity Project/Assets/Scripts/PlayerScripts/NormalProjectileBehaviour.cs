using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectileBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(10, 60)]
    [Tooltip("Projectile Speed")]
    private float TravelingSpeed;
    private GameObject test;
    public static bool hit = false;
    Vector3 demo;
    void Start()
    {
        test = GameObject.Find("ProjectileSpawningPointMain");
        demo = test.transform.forward;
    }

    void Update()
    {
        this.transform.position += demo* TravelingSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        if (collision.gameObject.name == "Enemy")
        {
            hit = true;
            EnemyDetection.isDetected = true;
            EnemyHealth.Damage(20);
            if (EnemyHealth.getHealth() <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }

}
