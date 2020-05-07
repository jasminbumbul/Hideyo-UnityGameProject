using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBehaviour : MonoBehaviour
{
    [Range(10, 60)]
    public float TravelingSpeed;
    bool collided = false;
    private GameObject CameraObject;
    Vector3 CameraVector;
    private Rigidbody Rigidbody;
    void Start()
    {
        CameraObject = GameObject.Find("MainCamera");
        CameraVector = CameraObject.transform.forward;
        Rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (collided)
        {
            StopMovement();
        }
        else
        {
            StartMovement();
        }
    }

    private void StopMovement()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    }


    private void StartMovement()
    {
        this.transform.position += CameraVector * TravelingSpeed * Time.deltaTime;
        this.transform.Rotate(Vector3.left * 720 * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
        Debug.Log("collided with "+ collision.gameObject.name);
     
    }

};
