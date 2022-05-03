using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Transform[] inventoryObjects = new Transform[5];
    private Transform[] inventoryIcons = new Transform[5];
    private void Awake()
    {
        
    }
    void PickItemUp(Transform obj)
    {
        DeleteFromScene(obj);
        //
        AddObjectToInventory(obj);
    }
    void RollCurrentIcon()
    {

    }
    private void DeleteFromScene(Transform obj)
    {
        throw new NotImplementedException();
    }

    private void AddObjectToInventory(Transform obj)
    {
        GetSprite(obj);
        //add to UI
    }
    private void DeleteFromInventory(Transform obj)
    {
        MoveItemsUp();
    }
    

    private void MoveItemsUp()
    {
        throw new NotImplementedException();
    }
    private void GetSprite(Transform obj)
    {

    }
}
