using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    //public Item Item;
    //private Light light;
    //public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    //{
    //    Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

    //    ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
    //    itemWorld.SetItem(item);

    //    return itemWorld;
    //}

    //private void Awake()
    //{
    //    light = GetComponent<Light>();
    //}

    //public void SetItem(Item item)
    //{
    //    this.Item = item;
    //    light.color = item.GetColor();
    //}

    //public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    //{
    //    Vector3 randomDir = UtilsClass.GetRandomDir();
    //    ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 8f, item);
    //    itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 40f, ForceMode2D.Impulse);
    //    return itemWorld;
    //}
}
