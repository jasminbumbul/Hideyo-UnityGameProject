using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public bool triggered = false;

    bool hasCoins = false;
    public GameObject InteractText;
    public GameObject ChangeableInteractText;
    float timer = 0.0f;
    Animator dialogueBoxAnimator;
    public GameObject MainCastleDoor;
    private bool check = false;
    private bool checkInteractText = false;

    public GameObject KatanaSlot;
    public GameObject BladeSlot;
    public GameObject MedKitSlot;
    public GameObject CoinSlot;
    private GameObject[] Chest;

    public AudioSource punchAudioSource;
    public AudioSource swordSlashAudioSource;
    public AudioSource bladeThrowAudioSource;
    public AudioSource openCloseInventoryAudioSource;
    public AudioSource HealAudioSource;

    public static Player instance;
    public float distanceBetwenPlayerAndHuman;

    public bool isPaused = false;
    private int counter = 0;

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
        dialogueTrigger = GameObject.Find("Trader").GetComponent<DialogueTrigger>();
        dialogueBoxAnimator = GameObject.Find("DialogueBox").GetComponent<Animator>();

        Chest = GameObject.FindGameObjectsWithTag("Chest");

        foreach (Item item in itemsToAdd)
        {
            myInventory.addItem(new ItemStack(item, 1));
        }

        //InventoryManager.INSTANCE.openContainer(new ContainerPlayerHotbar(null, myInventory));
        InventoryManager.INSTANCE.resetInventoryStatus();

        Cursor.lockState = CursorLockMode.Locked;
        instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer %= 60;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            openCloseInventoryAudioSource.Play();
            if (!InventoryManager.INSTANCE.hasInventoryCurrentlyOpen())
            {
                Cursor.lockState = CursorLockMode.None;
                InventoryManager.INSTANCE.openContainer(new ContainerPlayerInventory(null, myInventory));
            }
            else
            {
                InventoryManager.INSTANCE.closeInventory();
                Cursor.lockState = CursorLockMode.Locked;
            }
        }


        //for (int i = 0; i < hotbarControls.Length; i++)
        //{
        //    if (Input.GetKeyDown(hotbarControls[i]))
        //    {
        //        selectedHotbarIndex = i;
        //    }
        //}


        CheckForItems();

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Time.time > NextThrow)
            {
                foreach (var item in myInventory.getInventoryStacks())
                {
                    if (item.item != null)
                    {

                        if (item.item.name == "Blade" && item.count > 0 && timer > 1)
                        {
                            animator.SetTrigger("BladeTrigger");
                            NextThrow = Time.time + ThrowRate;
                            Invoke("SpawnBlade", 1);
                            item.decreaseAmount(1);
                            timer = 0;
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
                        KatanaSlot.SetActive(true);
                    }
                    break;
                }

            }

        }



        if (!postoji)
        {
            if (animator.GetBool("SwordOut"))
            {
                KatanaSlot.SetActive(false);
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
                    if (item.item.name == "MedKit" && item.count > 0 && timer > 1)
                    {
                        HealAudioSource.Play();
                        item.decreaseAmount(1);
                        Health.instance.health += 50;
                        break;
                    }
                }
            }

        }



        if (Input.GetMouseButtonDown(0) && animator.GetBool("SwordOut") && !animator.GetBool("IsRunning") && !animator.GetBool("JumpTrigger"))
        {
            if (timer > 0.8f)
            {
                animator.SetTrigger("SwordTrigger");
                swordSlashAudioSource.Play();
                timer = 0;
            }
        }


        if (Input.GetMouseButtonDown(0) && !animator.GetBool("SwordOut") && !animator.GetBool("IsWalking"))
        {
            if (timer > 0.6f)
            {
                animator.SetTrigger("PunchTrigger");
                punchAudioSource.Play();
                timer = 0;
            }
        }

        if (Input.GetMouseButton(1) && animator.GetBool("SwordOut"))
        {
            animator.SetBool("IsDefending", true);
        }
        else
        {
            animator.SetBool("IsDefending", false);
        }

        //interaction with humans
        distanceBetwenPlayerAndHuman = Vector3.Distance(this.transform.position, Human.transform.position);

        if (distanceBetwenPlayerAndHuman < 3)
        {
            InteractText.SetActive(true);
            if ((triggered == false && Input.GetKey(KeyCode.E)) || hasCoins && !triggered && counter == 0)
            {
                Cursor.lockState = CursorLockMode.None;
                CoinSlot.SetActive(true);
                AddCoinSlotUI();
                timer = 0.0f;
                if (hasCoins)
                {
                    dialogueTrigger.TriggerDialogue(hasCoins);
                    triggered = true;
                    counter++;
                }
                else
                {
                    dialogueTrigger.TriggerDialogue();
                    triggered = true;
                }
            }
        }


        if (!isPaused && (distanceBetwenPlayerAndHuman > 3 && triggered))
        {
            Cursor.lockState = CursorLockMode.Locked;
            triggered = false;
            dialogueTrigger.StopDialogue();
            InteractText.SetActive(false);
        }

        if (distanceBetwenPlayerAndHuman > 3 && !triggered)
        {
            InteractText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && distanceBetwenPlayerAndHuman < 3 && !dialogueBoxAnimator.GetBool("IsOpen"))
        {

            hasCoins = UnknownManInventory.INSTANCE.create(hasCoins);
            if (hasCoins)
            {
                triggered = false;
            }

        }

        if (SceneManager.GetActiveScene().name == "SecondScene")
        {

            //opening the main door with the key
            float distanceBetwenPlayerAndMainDoor = Vector3.Distance(this.transform.position, MainCastleDoor.transform.position);

            if (hasKey())
            {
                GameObject.Find("Odobreno").GetComponent<Light>().color = Color.green;
            }
            else
            {
                GameObject.Find("Odobreno").GetComponent<Light>().color = Color.red;
            }

            if (distanceBetwenPlayerAndMainDoor < 5)
            {
                InteractText.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    check = true;
                    if (hasKey())
                    {
                        decreaseKeyAmount();
                        //nova scena
                        Debug.Log("nova scena");
                    }

                    else
                    {
                        ChangeableInteractText.SetActive(true);
                        timer = 0;
                    }
                }
                if (check)
                {
                    InteractText.SetActive(false);
                }
            }
            else
            {
                // InteractText.SetActive(false);
                check = false;
            }
        }




        if (timer > 3)
        {
            ChangeableInteractText.SetActive(false);
        }

        //interakcija sa chestom
        float distance = 0f;
        float minDistance = 1000f;

        foreach (var chest in Chest)
        {
            distance = Vector3.Distance(transform.position, chest.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        if (minDistance < 4)
        {
            InteractText.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }





    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    private void AddCoinSlotUI()
    {
        int numberOfItems = 0;
        foreach (var item in myInventory.getInventoryStacks())
        {
            if (item.item != null)
            {
                if (item.item.name == "Coin")
                {
                    numberOfItems += item.count;
                }
            }
        }
        GameObject.Find("BladeSlotAmount").GetComponent<Text>().text = numberOfItems.ToString() + "/20";
    }

    private void CheckForItems()
    {
        bool postojiBlade = false;
        bool postojiMedKit = false;
        bool postojiCoin = false;
        int numberOfItems = 0;

        foreach (var item in myInventory.getInventoryStacks())
        {
            if (item.item != null)
            {
                if (item.item.name == "Blade")
                {
                    postojiBlade = true;
                    numberOfItems += item.count;
                    BladeSlot.SetActive(true);
                }
            }
        }
        if (postojiBlade)
        {
            GameObject.Find("BladeSlotAmount").GetComponent<Text>().text = numberOfItems.ToString();

        }
        if (numberOfItems <= 0)
        {
            BladeSlot.SetActive(false);
        }
        numberOfItems = 0;

        foreach (var item in myInventory.getInventoryStacks())
        {
            if (item.item != null)
            {
                if (item.item.name == "MedKit")
                {
                    postojiMedKit = true;
                    MedKitSlot.SetActive(true);
                    numberOfItems += item.count;
                }
            }
        }
        if (postojiMedKit)
        {
            GameObject.Find("MedKitSlotAmount").GetComponent<Text>().text = numberOfItems.ToString();
        }
        if (numberOfItems <= 0)
        {
            MedKitSlot.SetActive(false);
        }

        foreach (var item in myInventory.getInventoryStacks())
        {
            if (item.item != null)
            {
                if (item.item.name == "Coin")
                {
                    postojiCoin = true;
                    numberOfItems += item.count;
                }
            }
        }
        if (!postojiCoin)
        {
            numberOfItems = 0;
        }
        if (CoinSlot.activeSelf)
        {
            GameObject.Find("CoinSlotAmount").GetComponent<Text>().text = numberOfItems.ToString() + "/20";
            if (numberOfItems >= 20)
            {
                GameObject.Find("CoinSlotAmount").GetComponent<Text>().color = Color.green;
            }
            else
            {
                GameObject.Find("CoinSlotAmount").GetComponent<Text>().color = Color.red;
            }
        }


    }

    private bool hasKey()
    {
        foreach (var item in myInventory.getInventoryStacks())
        {
            if (item.item != null)
            {
                if (item.item.name == "Key")
                {
                    return true;

                }
            }
        }
        return false;
    }

    private void decreaseKeyAmount()
    {
        foreach (var item in myInventory.getInventoryStacks())
        {
            if (item.item != null)
            {
                if (item.item.name == "Key")
                {
                    item.decreaseAmount(item.count);
                }
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
            selectedHotbarIndex < 0; selectedHotbarIndex += 6) ;

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
        bladeThrowAudioSource.Play();
    }

}