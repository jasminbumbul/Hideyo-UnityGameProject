using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    public static List<GameObject> ObjectList=new List<GameObject>();
    public GameObject Katana;
    public GameObject Blade;
    //public GameObject Stick;
    //public GameObject MedKit;
    //public GameObject Coin;
    //public GameObject Key;
    //public GameObject Shirt;
    //public GameObject Pants;
    //public GameObject Shoes;
    //public GameObject Totem;


    private void Start ()
    {
        ObjectList.Add(Katana);
        ObjectList.Add(Blade);
        //ObjectList.Add(Stick);
        //ObjectList.Add(MedKit);
        //ObjectList.Add(Coin);
        //ObjectList.Add(Key);
        //ObjectList.Add(Shirt);
        //ObjectList.Add(Pants);
        //ObjectList.Add(Shoes);
        //ObjectList.Add(Totem);
    }

    public static List<GameObject> GetObjectList()
    {
        return ObjectList;
    }

    private void Awake()
    {
        Instance = this;
    }

    //public Transform pfItemWorld;

    public Sprite KatanaSprite;
    //public Sprite StickSprite;
    public Sprite BladeSprite;
    //public Sprite MedKitSprite;
    //public Sprite CoinSprite;
    //public Sprite KeySprite;
    //public Sprite ShirtSprite;
    //public Sprite PantsSprite;
    //public Sprite ShoesSprite;
    //public Sprite TotemSprite;
}
