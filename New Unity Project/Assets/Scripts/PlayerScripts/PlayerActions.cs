using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Animator Animator;
    //projectile settings
    public GameObject Blade;
    public Transform BladeContainer;
    public Transform BladeSpawnPoint;

    [Range(0, 2)]
    public float ThrowRate;
    private float NextThrow = 0.0f;




    float Timer = 0.0f;


    private bool InventoryIsOpen;




    private void Start()
    {
        Animator = GameObject.Find("HumanModel").GetComponent<Animator>();


    }
    private void Update()
    {
  

     

       



        //sword slash
    

        //if (Input.GetMouseButtonDown(1) && Animator.GetBool("SwordOut"))
        //{
        //    Animator.SetBool("IsDefending", true);
        //}
        //else
        //{
        //    Animator.SetBool("IsDefending", false);
        //}

       
    






    }


}
