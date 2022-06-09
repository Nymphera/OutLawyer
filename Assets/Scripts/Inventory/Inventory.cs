using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Inv : MonoBehaviour
{
    private const int SLOTS = 5;
    [SerializeField]
    private List<InventoryItem> invItems = new List<InventoryItem>();
    public Canvas canvas;

    public void AddItem(InventoryItem pickedItem)
    {

        if (invItems.Count < SLOTS)
        {
            Collider collider = GetComponent<Collider>();
            if (collider.enabled)
            {
                invItems.Add(pickedItem);
                pickedItem.OnPickup();
            }
        }
    }

    public InventoryItem RemoveItem(int itemIndex)
    {
        return invItems[itemIndex];
    }

    public void DisplayItems()
    {
        int counter = 0;
        Transform InventoryPanel = transform.Find("InvPanel");
        foreach (Transform slot in InventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            if (!image.enabled && invItems.Count <= counter)
            {
                image.enabled = true;
                image.sprite = invItems[counter].image;
                counter++;
            }
        }   
    }
}