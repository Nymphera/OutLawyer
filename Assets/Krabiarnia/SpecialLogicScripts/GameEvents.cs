using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Start()
    {
        current = this;
    }
    public event Action<int> onDoorTriggerEnter;
    public void DoorTriggerEnter(int id)
    {
        onDoorTriggerEnter(id);
    }
}