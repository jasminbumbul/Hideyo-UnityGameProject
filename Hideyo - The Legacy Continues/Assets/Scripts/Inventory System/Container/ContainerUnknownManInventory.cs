using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUnkownManInventory : Container
{

    public ContainerUnkownManInventory(Inventory containerInventory, Inventory playerInventory) : base(containerInventory, playerInventory)
    {
        //Player Hotbar Slots
        for (int i = 0; i < 6; i++)
        {
            addSlotToContainer(playerInventory, i, 35 + (50 * i), -90, 50);
        }

        //Player Inventory Slots Row 1
        for (int i = 0; i < 6; i++)
        {
            addSlotToContainer(playerInventory, 6 + i, 35 + (50 * i), 90, 50);
        }

        //Player Inventory Slots Row 2
        for (int i = 0; i < 6; i++)
        {
            addSlotToContainer(playerInventory, 12 + i, 35 + (50 * i), 40, 50);
        }

        //Player Inventory Slots Row 3
        for (int i = 0; i < 6; i++)
        {
            addSlotToContainer(playerInventory, 18 + i, 35 + (50 * i), -10, 50);
        }

        //man inv
         for(int i = 0; i < 1; i++)
        {
            addSlotToContainer(containerInventory, i, -170,10, 125);
        }

       
    }
    public override GameObject getContainerPrefab()
    {
        return InventoryManager.INSTANCE.getContainerPrefab("Unknown Inventory");
    }
}