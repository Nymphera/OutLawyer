using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private Transform[] inventoryObjects = new Transform[5];
    private Transform[] inventoryIcons = new Transform[5];

    private GameControls GameControls;
    private void Awake()
    {
        GameControls = new GameControls();

        GameControls.Game.Scroll.performed += RollThroughIcons;
    }

    private void RollThroughIcons(InputAction.CallbackContext obj)
    {
        Debug.Log("123");
        Debug.Log(GameControls.Game.Scroll.ReadValue<Vector2>());
    }

    void PickItemUp(Transform obj)
    {
        DeleteFromScene(obj);
        //
        AddObjectToInventory(obj);
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
