using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public int Amount;
    public enum ItemType
    {
        Katana,
        //Stick,
        Blade
        //MedKit,
        //Coin,
        //Key,
        //Shirt,
        //Pants,
        //Shoes,
        //Totem
    }

    public Item(int amount, ItemType itemType)
    {
        Amount = amount;
        this.itemType = itemType;
    }


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Katana: return ItemAssets.Instance.KatanaSprite;
            //case ItemType.Stick: return ItemAssets.Instance.StickSprite;
            case ItemType.Blade: return ItemAssets.Instance.BladeSprite;
                //case ItemType.MedKit: return ItemAssets.Instance.MedKitSprite;
                //case ItemType.Coin: return ItemAssets.Instance.CoinSprite;
                //case ItemType.Key: return ItemAssets.Instance.KeySprite;
                //case ItemType.Shirt: return ItemAssets.Instance.ShirtSprite;
                //case ItemType.Pants: return ItemAssets.Instance.PantsSprite;
                //case ItemType.Shoes: return ItemAssets.Instance.ShoesSprite;
                //case ItemType.Totem: return ItemAssets.Instance.TotemSprite;
        }
    } 
    
    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Katana: return new Color(0,0,0);
            //case ItemType.Stick:  return new Color(0,0,0);
            case ItemType.Blade:  return new Color(1,1,0);
            //case ItemType.MedKit: return new Color(0,1,1);
            //case ItemType.Coin:   return new Color(1,1,0);
            //case ItemType.Key:    return new Color(1,1,0);
            //case ItemType.Shirt:  return new Color(0,0,1);
            //case ItemType.Pants:  return new Color(0,0,1);
            //case ItemType.Shoes:  return new Color(0,0,1);
            //case ItemType.Totem:  return new Color(1, 1, 1);
        }
    }

    public bool IsStackable()
    {
        switch(itemType)
        {
            default:
            //case ItemType.Coin:
            case ItemType.Blade:
                //case ItemType.Totem:
                //case ItemType.MedKit:
                return true;
            case ItemType.Katana:
            //case ItemType.Stick:
            //case ItemType.Key:
            //case ItemType.Shirt:
            //case ItemType.Pants:
            //case ItemType.Shoes:
                return false;
        }
    }

    internal void Destroy()
    {
        this.Destroy();
    }
}
