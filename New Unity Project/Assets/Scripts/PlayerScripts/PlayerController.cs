using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public float MovingSpeed = 10.0f;
    public float RotationSpeed = 50.0f;
    private float FasterWalkingInput;
    private float Translation;
    private float Rotation;


    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        Translation = Input.GetAxis("Vertical") * MovingSpeed;
        Rotation = Input.GetAxis("Horizontal") * RotationSpeed;
     

        // Make it move 10 meters per second instead of 10 meters per frame...
        Translation *= Time.deltaTime;
        Rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, Translation);

        // Rotate around our y-axis
        transform.Rotate(0, Rotation, 0);


        //kod za brzo trcanje
        FasterWalkingInput = Input.GetAxis("FasterWalking");
        if (FasterWalkingInput == 1)
        {
            MovingSpeed = 20.0f;
        }
        else
        {
            MovingSpeed = 10.0f;
        }


        //moze se iskoristiti za zamah macem
        //float fire = Input.GetAxis("Fire1");
        //if (fire==1)
        //{
        //    if (Time.time> NextFire)
        //    {
        //        NextFire = Time.time + FireRate;
        //        GameObject.Instantiate(Projectile,ProjectileSpawnPoint.position,Projectile.transform.rotation,PlayerProjectileContainer);
        //    }
        //}

    }
}
