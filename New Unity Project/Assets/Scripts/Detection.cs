using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (this.transform.gameObject.name == "HumanModal" && Input.GetKeyDown(KeyCode.Mouse1))
        {
        }
        else
        {


            if (other.transform.gameObject.name == "Sphere")
            {

                this.gameObject.GetComponent<Health>().healt -= 20;

            }
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
