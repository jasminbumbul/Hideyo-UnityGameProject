using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrmarInventory:MonoBehaviour
{
    public event EventHandler OnItemListChangedOrmar;
    private List<Item> ItemList;

    public Item[] itemsInOrmar;
    public int[] Amount;
    private int index=0;
    public OrmarInventory()
    {
        ItemList = new List<Item>();

        foreach (var item in itemsInOrmar)
        {
            Item temp= new Item(Amount[index],item.itemType);
            AddItem(temp);
            index++;
        }





    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool ItemAlreadyInInventory = false;
            foreach (Item InventoryItem in ItemList)
            {
                if (InventoryItem.itemType == item.itemType)
                {
                    InventoryItem.Amount += item.Amount;
                    ItemAlreadyInInventory = true;
                }
            }
            if (!ItemAlreadyInInventory)
            {
                ItemList.Add(item);

            }
        }
        else
        {
            ItemList.Add(item);


        }
        OnItemListChangedOrmar?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in ItemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.Amount -= 1;
                    itemInInventory = inventoryItem;
                }
            }

            if (itemInInventory != null && itemInInventory.Amount <= 0)
            {
                ItemList.Remove(itemInInventory);
            }
        }
        else
        {
            ItemList.Remove(item);
        }



        OnItemListChangedOrmar?.Invoke(this, EventArgs.Empty);

    }


    public List<Item> GetItemList()
    { return ItemList; }


}
