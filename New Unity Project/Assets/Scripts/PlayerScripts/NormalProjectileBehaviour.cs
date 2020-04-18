using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectileBehaviour : MonoBehaviour
{
    //private Rigidbody rigidbody;
    [SerializeField]
    [Range(10, 60)]
    [Tooltip("Projectile Speed")]
    private float TravelingSpeed;
    private GameObject test;
    Vector3 demo;
    void Start()
    {
        test = GameObject.Find("ProjectileSpawningPoint");
        demo = test.transform.forward;
        //rigidbody = GetComponent<Rigidbody>();
        //rigidbody.freezeRotation = true;
    }

    void Update()
    {
        this.transform.position += demo * TravelingSpeed * Time.deltaTime;
    }

}
