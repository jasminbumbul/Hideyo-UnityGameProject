using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;


public class UI_OrmarInventory : MonoBehaviour
{
    private OrmarInventory OrmarInventory;
    private Transform ItemSlotContainer;
    private Transform ItemSlotTemplate;



    private void Awake()
    {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");

    }
    public void SetInventory(OrmarInventory OrmarInventory)
    {
        this.OrmarInventory = OrmarInventory;

        OrmarInventory.OnItemListChangedOrmar += OrmarInventory_OnItemListChanged;
        RefreshInventoryItems();

    }

    private void OrmarInventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in ItemSlotContainer)
        {
            if (child == ItemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float ItemSlotCellSize = 130f;
        foreach (Item item in OrmarInventory.GetItemList())
        {
            RectTransform ItemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            ItemSlotRectTransform.gameObject.SetActive(true);

            //ItemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => { };
            ItemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Item duplicateItem = new Item(item.Amount,item.itemType); 

                ////transfer item to inv
                PickUpSystem.MoveItemToInventory(duplicateItem);

                //drop item
                OrmarInventory.RemoveItem(item);

            };



            ItemSlotRectTransform.anchoredPosition = new Vector2(x * ItemSlotCellSize, y * 80);

            Image image = ItemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI UIText = ItemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
            if (item.Amount > 1)
            {
                UIText.SetText(item.Amount.ToString());
            }
            else
            {
                UIText.SetText("1");
            }

            x++;
            if (x > 2)
            {
                x = 0;
                y++;
            }
        }
    }

    private void TransferToInventory(Item item)
    {
        

    }
}
