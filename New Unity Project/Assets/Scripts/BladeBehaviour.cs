using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBehaviour : MonoBehaviour
{
    [Range(10, 60)]
    public float TravelingSpeed;

    private GameObject test;
    Vector3 demo;
    void Start()
    {
        test = GameObject.Find("MainCamera");
        demo = test.transform.forward;
    }
    private void Update()
    {
        this.transform.position += demo * TravelingSpeed * Time.deltaTime;
        this.transform.rotation = test.transform.rotation ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        //if (collision.gameObject.name == "MainTank")
        //{
        //    PlayerHealth.Damage(20);
        //    if (PlayerHealth.getHealth() <= 0)
        //    {
        //        Destroy(collision.gameObject);
        //    }
        //}
    }

}
