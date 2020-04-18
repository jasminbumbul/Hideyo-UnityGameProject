using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTankController: MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 50.0f;

    public GameObject Projectile;
    public Transform PlayerProjectileContainer;
    public Transform ProjectileSpawnPoint;

    [Tooltip("Projectile fire rate")]
    [Range(0,1)]
    public float FireRate;
    private float NextFire = 0.0f;
    

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

        float fire = Input.GetAxis("Fire1");
        if (fire==1)
        {
            if (Time.time> NextFire)
            {
                NextFire = Time.time + FireRate;
                GameObject.Instantiate(Projectile,ProjectileSpawnPoint.position,Projectile.transform.rotation,PlayerProjectileContainer);
            }
        }
        
    }
}
