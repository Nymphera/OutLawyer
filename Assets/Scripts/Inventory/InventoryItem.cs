using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    //string Tag { get; }


    void OnPickup();

}
