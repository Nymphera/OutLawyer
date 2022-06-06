using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{   [SerializeField]
    List<GameObject> interactable = new List<GameObject>();
    private void Awake()
    {
        SetInteractables();
    }

    private void SetInteractables()
    {
      interactable.AddRange(GameObject.FindGameObjectsWithTag("Interact"));
        SetOutlines();
    }

    private void SetOutlines()
    {
        foreach(GameObject obj in interactable)
        {
           
        }
    }
}
