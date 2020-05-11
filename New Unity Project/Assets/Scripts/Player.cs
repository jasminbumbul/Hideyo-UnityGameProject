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
    bool tempFlag = false;
    bool vecDodano = false;
    bool postoji = false;

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

        foreach (Item item in itemsToAdd)
        {
            myInventory.addItem(new ItemStack(item, 1));
        }

        //InventoryManager.INSTANCE.openContainer(new ContainerPlayerHotbar(null, myInventory));
        InventoryManager.INSTANCE.resetInventoryStatus();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!InventoryManager.INSTANCE.hasInventoryCurrentlyOpen())
            {
                Cursor.lockState = CursorLockMode.None;
                InventoryManager.INSTANCE.openContainer(new ContainerPlayerInventory(null, myInventory));
            }
        }


        for (int i = 0; i < hotbarControls.Length; i++)
        {
            if (Input.GetKeyDown(hotbarControls[i]))
            {
                selectedHotbarIndex = i;
            }
        }




        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Time.time > NextThrow)
            {
                foreach (var item in myInventory.getInventoryStacks())
                {
                    if (item.item.name == "Blade" && item.count > 0)
                    {
                        animator.SetTrigger("BladeTrigger");
                        NextThrow = Time.time + ThrowRate;
                        Invoke("SpawnBlade", 1);
                        item.decreaseAmount(1);
                        //if (item.isEmpty())
                        //{
                        //    myInventory.getInventoryStacks().Remove(item);
                        //}
                        //if(Player.myInventory.getInventoryStacks().Count==0)
                        //{
                        //    Player.myInventory.getInventoryStacks().Remove(item);
                        //}
                        //Player.myInventory.removeItem(new ItemStack(item.item, 1));


                    }
                }
            }
        }
        //Debug.Log(tempFlag);   

        int itemcount;
        Debug.Log(postoji);
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
                        GameObject.Instantiate(Katana, GameObject.Find("KatanaHolder").transform.position, GameObject.Find("KatanaHolder").transform.rotation, GameObject.Find("MiddleHand.R").transform);
                    }
                    break;
                }

            }

        }
        Debug.Log(postoji);



        if (!postoji)
        {
            if (animator.GetBool("SwordOut"))
            {
                if (GameObject.Find("Katana") != null)
                {
                    animator.SetBool("SwordOut", false);
                    var children = new List<GameObject>();
                    foreach (Transform child in GameObject.Find("MiddleHand.R").transform) children.Add(child.gameObject);
                    //children.ForEach(child => Destroy(child));
                    foreach (var child in children)
                    {
                        if (child.name=="Katana")
                        {
                            Destroy(child);
                        }
                    }

                    postoji = false;

                }
            }
        }







        if (Input.GetMouseButton(0) && animator.GetBool("SwordOut"))
        {
            animator.SetBool("SwordSlash", true);
        }
        else
        {
            animator.SetBool("SwordSlash", false);
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