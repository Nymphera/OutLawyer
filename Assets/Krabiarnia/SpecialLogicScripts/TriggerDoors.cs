using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoors : MonoBehaviour
{
    [SerializeField]
    private int objectID;
    private void OnMouseDown()
    {          
            GameEvents.current.DoorTriggerEnter(objectID);      
    }
}
