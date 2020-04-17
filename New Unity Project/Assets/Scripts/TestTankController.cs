using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTankController: MonoBehaviour
{

    //public float speed;
    //private float HorizontalMovement = 0f;
    //private float VerticalMovement   = 0f;
    //private Rigidbody rigidBody;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    rigidBody = GetComponent<Rigidbody>();
    //    rigidBody.freezeRotation = true;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    HorizontalMovement = Input.GetAxis("Horizontal");
    //    VerticalMovement = Input.GetAxis("Vertical");
    //    if (HorizontalMovement > 0 || HorizontalMovement < 0)
    //    {
    //        rigidBody.velocity = new Vector2(speed * HorizontalMovement, rigidBody.velocity.y);
    //    }
    //    if (VerticalMovement > 0 || VerticalMovement < 0)
    //    {
    //        rigidBody.velocity = new Vector2(speed * VerticalMovement, rigidBody.velocity.z);
    //    }
    //}

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }
}
