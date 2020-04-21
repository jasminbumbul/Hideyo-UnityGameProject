using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    private static int Health = 100;

 
    public static int getHealth() => Health;

    public static void Damage(int DamageAmount)
    {
        Health -= DamageAmount;
        ReduceHealthBar(DamageAmount);
    }

    private static void ReduceHealthBar(int damageAmount)
    {
        GameObject.Find("MainTankHealthBar").transform.localScale -= new Vector3(0, 0, 0.21f);
    }
}