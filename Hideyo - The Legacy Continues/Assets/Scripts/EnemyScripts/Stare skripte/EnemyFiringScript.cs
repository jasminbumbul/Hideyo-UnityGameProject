//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class EnemyFiringScript : MonoBehaviour
//{

//    [SerializeField]
//    [Range(10, 60)]
//    [Tooltip("Projectile Speed")]
//    private float TravelingSpeed;
//    private GameObject test;
//    Vector3 demo;
//    void Start()
//    {
//        test = GameObject.Find("ProjectileSpawningPointEnemy");
//        demo = test.transform.forward;
        
//    }

//    void Update()
//    {
//        this.transform.position += demo * TravelingSpeed * Time.deltaTime;
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        Destroy(this.gameObject);
//        if (collision.gameObject.name == "MainTank")
//        {
//            PlayerHealth.Damage(20);
//            if (PlayerHealth.getHealth()<=0)
//            {
//                Destroy(collision.gameObject);
//            }
//        }
//    }
//}
