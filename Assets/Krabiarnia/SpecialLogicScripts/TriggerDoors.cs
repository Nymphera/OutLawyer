using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoors : MonoBehaviour
{
    [SerializeField]
    private int objectID;
    [SerializeField]
    private bool isDoorOpened=false;
    private void OnMouseDown()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,10f))
        {
            if (hit.transform.gameObject == gameObject)
            {
                GameEvents.current.DoorMouseClick(objectID,isDoorOpened);
                this.isDoorOpened = !isDoorOpened;
            }
        }
           
    }
    

}
