using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> ItemList;



    public Inventory()
    {
        ItemList = new List<Item>();
        //AddItem(new Item { itemType = Item.ItemType.Katana, Amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.Blade, Amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.Katana, Amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.Blade, Amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.Blade, Amount = 1 });



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
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

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
                    inventoryItem.Amount -=1;
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



        OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }


    public List<Item> GetItemList()
    { return ItemList; }


}
