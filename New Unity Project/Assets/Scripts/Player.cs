using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Item[] itemsToAdd;

    public static Inventory myInventory = new Inventory(24);
    private int selectedHotbarIndex = 0;
    private Animator animator;

    public static Player Instance;

    //projectile settings
    public GameObject Blade;
    public Transform BladeContainer;
    public Transform BladeSpawnPoint;

    [Range(0, 2)]
    public float ThrowRate;
    private float NextThrow = 0.0f;

    public GameObject Katana;
    bool vecDodano = false;
    bool postoji = false;

    public GameObject Human;
    private DialogueTrigger dialogueTrigger;
    private bool triggered = false;

    bool hasCoins = false;
    public GameObject InteractText;
    float timer = 0.0f;
    Animator dialogueBoxAnimator;

    private void Awake()
    {
        Instance = this;
    }

    private KeyCode[] hotbarControls = new KeyCode[]
    {
        KeyCode.Alpha1, //Key 1
        KeyCode.Alpha2, //Key 2
        KeyCode.Alpha3, //Key 3
        KeyCode.Alpha4, //Key 4
        KeyCode.Alpha5, //Key 5
        KeyCode.Alpha6 //Key 6
    };

    private void Start()
    {
        animator = GameObject.Find("HumanModel").GetComponent<Animator>();
        dialogueTrigger=GameObject.Find("Trader").GetComponent<DialogueTrigger>();
        dialogueBoxAnimator = GameObject.Find("DialogueBox").GetComponent<Animator>(); 

        foreach (Item item in itemsToAdd)
        {
            myInventory.addItem(new ItemStack(item, 1));
        }

        //InventoryManager.INSTANCE.openContainer(new ContainerPlayerHotbar(null, myInventory));
        InventoryManager.INSTANCE.resetInventoryStatus();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer %= 60;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!InventoryManager.INSTANCE.hasInventoryCurrentlyOpen())
            {
                InventoryManager.INSTANCE.openContainer(new ContainerPlayerInventory(null, myInventory));
            }
            else
            {
                InventoryManager.INSTANCE.closeInventory();
            }
        }


        //for (int i = 0; i < hotbarControls.Length; i++)
        //{
        //    if (Input.GetKeyDown(hotbarControls[i]))
        //    {
        //        selectedHotbarIndex = i;
        //    }
        //}




        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Time.time > NextThrow)
            {
                foreach (var item in myInventory.getInventoryStacks())
                {
                    if (item.item != null)
                    {

                        if (item.item.name == "Blade" && item.count > 0)
                        {
                            animator.SetTrigger("BladeTrigger");
                            NextThrow = Time.time + ThrowRate;
                            Invoke("SpawnBlade", 1);
                            item.decreaseAmount(1);
                            break;
                        }
                    }
                }
            }
        }

        foreach (var item in myInventory.getInventoryStacks())
        {
            postoji = false;
            if (item.item != null)
            {
                if (item.item.name == "Katana")
                {
                    postoji = true;
                    if (!vecDodano)
                    {
                        animator.SetBool("SwordOut", true);
                        vecDodano = true;
                        GameObject.Instantiate(Katana, GameObject.Find("KatanaHolder").transform.position, GameObject.Find("KatanaHolder").transform.rotation, GameObject.FindWithTag("Zglob").transform);
                    }
                    break;
                }

            }

        }



        if (!postoji)
        {
            if (animator.GetBool("SwordOut"))
            {
                animator.SetBool("SwordOut", false);
                var children = new List<GameObject>();
                foreach (Transform child in GameObject.FindWithTag("Zglob").transform) children.Add(child.gameObject);
                //children.ForEach(child => Destroy(child));
                foreach (var child in children)
                {
                    if (child.name == "Katana")
                    {
                        Destroy(child);
                    }
                }

                postoji = false;
                vecDodano = false;

            }
        }

        //koristenje medkita
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (var item in myInventory.getInventoryStacks())
            {
                if (item.item != null)
                {
                    if (item.item.name == "MedKit" && item.count > 0)
                    {
                        item.decreaseAmount(1);
                        Health.instance.health+=50;
                        break;
                    }
                }
            }

        }



        if (Input.GetMouseButton(0) && animator.GetBool("SwordOut"))
        {
            animator.SetTrigger("SwordTrigger");
        }


        if (Input.GetMouseButton(0) && !animator.GetBool("SwordOut"))
        {
            animator.SetTrigger("PunchTrigger");
        }

        if (Input.GetMouseButton(1) && animator.GetBool("SwordOut"))
        {
            animator.SetBool("IsDefending",true);
        }
        else{
            animator.SetBool("IsDefending",false);
        }

        //interaction with humans
        float distanceBetwenPlayerAndHuman=Vector3.Distance(this.transform.position,Human.transform.position);


        if (distanceBetwenPlayerAndHuman < 3 && triggered == false && Input.GetKey(KeyCode.E))
        {
            timer = 0.0f;
            if (hasCoins)
            {
                dialogueTrigger.TriggerDialogue(hasCoins);
                triggered = true;
            }
            else
            {
                dialogueTrigger.TriggerDialogue();
                triggered = true;
            }
        }

        if (distanceBetwenPlayerAndHuman < 3)
        {
            InteractText.SetActive(true);
        }
        else
        {
            InteractText.SetActive(false);
        }


        if (distanceBetwenPlayerAndHuman > 3)
        {
            triggered = false;
        }

        Debug.Log(InventoryManager.INSTANCE.hasInventoryCurrentlyOpen());
        if (Input.GetKeyDown(KeyCode.E) && distanceBetwenPlayerAndHuman < 3 && !dialogueBoxAnimator.GetBool("IsOpen"))
        {
          
                hasCoins = UnknownManInventory.INSTANCE.create(hasCoins);
                if (hasCoins)
                {
                    triggered = false;
                }
          
        }









    }



    private void updateSelectedHotbarIndex(float direction)
    {
        if (direction > 0)
            direction = 1;
            
        if (direction < 0)
            direction = -1;

        for (selectedHotbarIndex -= (int)direction;
            selectedHotbarIndex < 0; selectedHotbarIndex += 6);

        while (selectedHotbarIndex >= 6)
            selectedHotbarIndex -= 6;
    }

    public int getSelectedHotbarIndex()
    {
        return selectedHotbarIndex;
    }

    public Inventory getInventory()
    {
        return myInventory;
    }



    private void SpawnBlade()
    {
        GameObject.Instantiate(Blade, BladeSpawnPoint.position, Blade.transform.rotation, BladeContainer);


    }

}