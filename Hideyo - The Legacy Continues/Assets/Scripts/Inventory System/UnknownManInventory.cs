using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownManInventory : MonoBehaviour
{

    private Player player;
    public AudioSource openCloseInventoryAudioSource;
    private InventoryManager inventoryManager;
    public Inventory inventory = new Inventory(1);

    public static UnknownManInventory INSTANCE;

    private void Awake()
    {
        INSTANCE = this;
    }

 
    void Start ()
    {
        inventoryManager = InventoryManager.INSTANCE;
        player=FindObjectOfType<Player>();
    }

    public bool create(bool hasCoins)
    {
        hasCoins = false;
        if (!InventoryManager.INSTANCE.hasInventoryCurrentlyOpen())
        {
            openCloseInventoryAudioSource.Play();
            InventoryManager.INSTANCE.openContainer(new ContainerUnkownManInventory(inventory, player.getInventory()));
        }
        else
        {
            if (CheckIfEnoughCoins())
            {
                hasCoins = true;
            }
            else
            {
                hasCoins = false;
            }
            InventoryManager.INSTANCE.closeInventory();
            openCloseInventoryAudioSource.Play();
        }
        return hasCoins;
    }

      private bool CheckIfEnoughCoins()
      {
        foreach (var item in inventory.getInventoryStacks())
        {
            if (item.item != null)
            {

                if (item.item.name == "Coin" && item.count >= 20)
                {
                    item.decreaseAmount(item.count);
                    return true;
                }
            }
        }
        return false;
    }

}
