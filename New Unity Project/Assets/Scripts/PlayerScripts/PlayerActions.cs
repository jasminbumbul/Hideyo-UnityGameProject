using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //projectile settings
    public GameObject Blade;
    public Transform BladeContainer;
    public Transform BladeSpawnPoint;

    [Range(0, 1)]
    public float ThrowRate;
    private float NextThrow = 0.0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (Time.time > NextThrow)
            {
                NextThrow = Time.time + ThrowRate;
                GameObject.Instantiate(Blade, BladeSpawnPoint.position, Blade.transform.rotation, BladeContainer);
            }
        }
    }
}
