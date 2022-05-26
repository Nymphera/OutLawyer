using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inv : MonoBehaviour
{
    private const int SLOTS = 5;
    [SerializeField]
    private List<InventoryItem> invItems = new List<InventoryItem>();

    public void AddItem(InventoryItem pickedItem)
    {

        if (invItems.Count < SLOTS)
        {
            Collider collider = GetComponent<Collider>();
            if (collider.enabled)
            {

                invItems.Add(pickedItem);

                pickedItem.OnPickup();
                Console.WriteLine("This is C#");

            }
        }
    }

}