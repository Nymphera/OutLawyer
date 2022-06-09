using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItem
{
    string name { get; }
    Sprite image { get; }

 


    void OnPickup();

}
