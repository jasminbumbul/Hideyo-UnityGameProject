using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private Camera Camera;
    //[SerializeField]
    //private Camera RayCamera;
    [SerializeField]
    private LayerMask LayerMask;
    [SerializeField]
    private float PickUpTime = 2f;
    [SerializeField]
    private RectTransform pickuptext;
    //[SerializeField]
    //private Image PickUpProgressImage;
    //[SerializeField]
    //private TextMeshProUGUI ItemNameText;

    private static Item ItemBeingPickedUp;
    private float CurrentPickupTimerElapsed;

    [SerializeField] private UI_Inventory ui_Inventory;
    private static Inventory inventory;
    //[SerializeField] private UI_OrmarInventory ui_OrmarInventory;
    //private static OrmarInventory ormarInventory;

    //public GameObject OrmaricUI;

    private void Awake()
    {
        inventory = new Inventory();
        ui_Inventory.SetInventory(inventory);
        //ormarInventory = new OrmarInventory();
        //ui_OrmarInventory.SetInventory(ormarInventory);

    }

    private void Update()
    {
        SelectItemBeingPickedUpFromRay();
        if (HasItemTargetted())
        {
            //if (ItemBeingPickedUp.gameObject.name != "ormaric")
            //{
                pickuptext.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {

                    IncrementPickUpProgressAndTryComplete();
                }
                else
                {
                    CurrentPickupTimerElapsed = 0f;
                }
            //}
            //else
            //{
            //    if (OrmaricUI.activeSelf)
            //    {
            //        OrmaricUI.gameObject.SetActive(false);
            //        Cursor.lockState = CursorLockMode.Locked;

            //    }
            //    else
            //    {
            //        OrmaricUI.gameObject.SetActive(true);
            //        Cursor.lockState = CursorLockMode.None;

            //    }
            //}
        }
        else
        {
            pickuptext.gameObject.SetActive(false);
            CurrentPickupTimerElapsed = 0f;
        }
    }



    private void IncrementPickUpProgressAndTryComplete()
    {
        CurrentPickupTimerElapsed += Time.deltaTime;
        if (CurrentPickupTimerElapsed >= PickUpTime)
        {
            MoveItemToInventory();
        }
    }

    public static void MoveItemToInventory(Item itemFromOrmar=null)
    {
        //if (itemFromOrmar!=null)
        //{
        //    inventory.AddItem(itemFromOrmar);
        //}
        //else
        //{
            inventory.AddItem(ItemBeingPickedUp);
            ItemBeingPickedUp.gameObject.SetActive(false);
        //}

    }


    private bool HasItemTargetted()
    {
        return ItemBeingPickedUp != null;
    }

    private void SelectItemBeingPickedUpFromRay()
    {
        Ray Ray = Camera.ViewportPointToRay(Vector3.one / 2f);

        Debug.DrawRay(Ray.origin, Ray.direction * 10f, Color.red);

        RaycastHit HitInfo;

        if (Physics.Raycast(Ray, out HitInfo, 20f, LayerMask))
        {
          
            var HitItem = HitInfo.collider.GetComponent<Item>();

            if (HitItem == null)
            {
                ItemBeingPickedUp = null;
            }
            else if (HitItem != null && HitItem != ItemBeingPickedUp)
            {
                ItemBeingPickedUp = HitItem;

            }
        }
        else
        {
            ItemBeingPickedUp = null;
        }
    

   
    }
}
