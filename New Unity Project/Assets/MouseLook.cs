using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public Transform PlayerBody;
    float xRotation = 0f;

    private void Start()
    {
        //zakljucava kursor da ne izlazi van game view
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        //limit da ne ide previse lijevo,desno...
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * MouseX);
    }
}
