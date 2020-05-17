using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   
    public /*static*/ Item[] Items;
    public static ItemAssets instance;

    private void Awake()
    {
        instance = this;
    }

    public /*static*/ Item getItem(string ItemName)
    {
        foreach(var item in Items)
        {
            if (item.name==ItemName)
            {
                return item;
            }
        }
        return null;
    }

    public /*static*/ Item[] getItems()
    {
        return Items;
    }
}
