using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    GameObject demo;

    // Update is called once per frame
    void Update()
    {
        demo = GameObject.Find("testDoor");
        //if (Input.GetButton("O"))
        //{
        demo.transform.Rotate(demo.transform.position, 90);
        //}
    }
}
