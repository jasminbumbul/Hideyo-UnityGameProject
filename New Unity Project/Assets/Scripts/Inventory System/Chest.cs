using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Player player;
    private InventoryManager inventoryManager;
    private Inventory inventory = new Inventory(24);

    [SerializeField]
    private GameObject OpenContainerText;
    void Start ()
    {
        player = FindObjectOfType<Player>();
        inventoryManager = InventoryManager.INSTANCE;
    }


    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 4 && !inventoryManager.hasInventoryCurrentlyOpen())
        {
            OpenContainerText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventoryManager.openContainer(new ContainerChest(inventory, player.getInventory()));
            }

        }
        else
        {
            OpenContainerText.gameObject.SetActive(false);
        }
   
    }
}