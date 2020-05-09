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

    public GameObject Inventory;

    //private bool InventoryIsOpen = false;

  


    private void Start()
    {
        Animator = GameObject.Find("HumanModel").GetComponent<Animator>();
        Inventory.gameObject.SetActive(false);

        //StartKatanaTransform = KatanaClone.transform;
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        Timer %= 60;

        //bacanje blade-a
        if (Input.GetKeyDown(KeyCode.Y) && Timer>2)
        {
            if (Time.time > NextThrow)
            {
                Animator.SetBool("IsThrowing", true);
                NextThrow = Time.time + ThrowRate;
                Timer = 0;
                Invoke("SpawnBlade", 1);
            }
        }
        else
        {
            Animator.SetBool("IsThrowing", false);
        }

        //izlvaci mac
        if (Input.GetKeyDown(KeyCode.X) && !Animator.GetBool("SwordOut") && Timer>2)
        {
            Animator.SetBool("SwordOut", true);
            Invoke("MakeChild", 1f);
            Timer = 0;

        }
        else
        if (Input.GetKey(KeyCode.X) && Animator.GetBool("SwordOut") && Timer > 2)
        {
            Animator.SetBool("SwordOut", false);
            Invoke("RemoveChild", 1f);
            Timer = 0;

        }
        if (Animator.GetBool("SwordOut"))
        {
            Animator.SetBool("IdleSword", true);
        }
        else
        {
            Animator.SetBool("IdleSword", false);
        }

        //punch
        if (Input.GetMouseButton(0) && !Animator.GetBool("SwordOut"))
        {
            Animator.SetBool("IsPunching", true);
        }
        else
        {
            Animator.SetBool("IsPunching", false);
        }
        
        //sword slash
        if (Input.GetMouseButton(0) && Animator.GetBool("SwordOut") && Timer>3)
        {
            Animator.SetBool("SwordSlash", true);
        }
        else
        {
            Animator.SetBool("SwordSlash", false);
        }

        //if (Input.GetMouseButtonDown(1) && Animator.GetBool("SwordOut"))
        //{
        //    Animator.SetBool("IsDefending", true);
        //}
        //else
        //{
        //    Animator.SetBool("IsDefending", false);
        //}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Inventory.activeSelf)
            {
                Inventory.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;

            }
            else
            {
                Inventory.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;

            }
        }
       





    }

    private void MakeChild()
    {
        GameObject.Find("Katana").transform.parent = GameObject.Find("Palm.R").transform;
    }
    private void RemoveChild()
    {
        GameObject.Find("Katana").transform.parent = null;
        GameObject.Find("Katana").transform.parent = GameObject.Find("HumanModel").transform;
    }
    private void SpawnBlade()
    {
        GameObject.Instantiate(Blade, BladeSpawnPoint.position, Blade.transform.rotation, BladeContainer);
    }
}
