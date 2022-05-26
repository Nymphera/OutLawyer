using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryExample : MonoBehaviour, InventoryItem
{
    [SerializeField]
    private string name = "example";

    public string Name
    {
        get
        {
            return name;
        }
    }

    public Sprite Image => throw new System.NotImplementedException();

    public void OnPickup()
    {
        Destroy(gameObject);
    }
}
