using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InventoryItem itemToPickUp = null;
    public Inv inventory = new Inv();



    void Start()
    {
        inventory = gameObject.AddComponent<Inv>();
    }


    // Update is called once per frame
    void Update()
    {
  

        if (Input.GetKeyDown(KeyCode.E) && itemToPickUp != null)
        {
            inventory.AddItem(itemToPickUp);
            
            itemToPickUp = null;

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");

            itemToPickUp = other.GetComponent<InventoryItem>();


    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        itemToPickUp = null;
    }
}
