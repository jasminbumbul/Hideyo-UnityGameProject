using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObject;
    public float ClampAngle = 80.0f;
    public float InputSensitivity = 150.0f;
    public GameObject CameraObject;
    public GameObject PlayerObject;
    public float MouseX;
    public float MouseY;
    public float SmoothX;
    public float SmoothY;
    private float RotX = 0.0f;
    private float RotY = 0.0f;
    Animator animator;

    void Start()  
    {
        Vector3 Rotation = transform.localRotation.eulerAngles;
        RotX = Rotation.x;
        RotY = Rotation.y;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GameObject.Find("HumanModel").GetComponent<Animator>();
    }

    void Update()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        RotY += MouseX * InputSensitivity * Time.deltaTime;
        RotX -= MouseY * InputSensitivity * Time.deltaTime;

        RotX = Mathf.Clamp(RotX, -ClampAngle, ClampAngle);

        Quaternion LocalRotation = Quaternion.Euler(RotX, RotY, 0.0f);
        transform.rotation = LocalRotation;

    }

    private void LateUpdate()
    {
        
        CameraUpdater();
    }

    private void CameraUpdater()
    {
        Transform Target = CameraFollowObject.transform;
        float Step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.position, Step);
        // if (animator.GetBool("IsWalking")) ;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        GameObject.Find("MainPlayer").transform.rotation = Quaternion.Euler(0.0f, RotY, 0.0f);

    }
}
