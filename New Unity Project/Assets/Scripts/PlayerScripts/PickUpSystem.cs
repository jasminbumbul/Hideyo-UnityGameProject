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
    [SerializeField]
    private LayerMask LayerMask;
    [SerializeField]
    private float PickUpTime = 2f;
    [SerializeField]
    private RectTransform PickUpImageRoot;
    //[SerializeField]
    //private Image PickUpProgressImage;
    [SerializeField]
    private TextMeshProUGUI ItemNameText;

    private Item ItemBeingPickedUp;
    private float CurrentPickupTimerElapsed;


    private void Update()
    {
        SelectItemBeingPickedUpFromRay();

        if (HasItemTargetted())
        {
            PickUpImageRoot.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("pressed");
                IncrementPickUpProgressAndTryComplete();
            }
            else
            {
                CurrentPickupTimerElapsed = 0f;
            }

            //UpdatePickUpProgressImage();
        }
        else
        {
            PickUpImageRoot.gameObject.SetActive(false);
            CurrentPickupTimerElapsed = 0f;
        }
    }

 

    private void IncrementPickUpProgressAndTryComplete()
    {
        CurrentPickupTimerElapsed += Time.deltaTime;
        Debug.Log(CurrentPickupTimerElapsed);
        Debug.Log(PickUpTime);
        if (CurrentPickupTimerElapsed>=PickUpTime)
        {
            MoveItemToInventory();
        }
    }

    private void MoveItemToInventory()
    {
        Destroy(ItemBeingPickedUp.gameObject);
        ItemBeingPickedUp = null;
    }

    private bool HasItemTargetted()
    {
        return ItemBeingPickedUp != null;
    }
    //private void UpdatePickUpProgressImage()
    //{
    //    float pct = CurrentPickupTimerElapsed / PickUpTime;
    //    PickUpProgressImage.fillAmount = pct;
    //}

    private void SelectItemBeingPickedUpFromRay()
    {
        Ray Ray = Camera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(Ray.origin, Ray.direction * 5f, Color.red);

        RaycastHit HitInfo;
        if (Physics.Raycast(Ray,out HitInfo,5f,LayerMask))
        {
            var HitItem = HitInfo.collider.GetComponent<Item>();

            if (HitItem==null)
            {
                ItemBeingPickedUp = null;
            }
            else if (HitItem!=null && HitItem != ItemBeingPickedUp)
            {
                ItemBeingPickedUp = HitItem;
                ItemNameText.text = "Pickup " + ItemBeingPickedUp.gameObject.name;
            }
        }
        else
        {
            ItemBeingPickedUp = null;
        }
    }
}
