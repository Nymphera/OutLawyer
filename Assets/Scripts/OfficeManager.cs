using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OfficeManager : MonoBehaviour
{
    public event EventHandler OnPinBoardClicked;
    [SerializeField]
    private InputAction action;
    private Ray ray;
    private RaycastHit hitpoint;
    Vector3 mousePosition;
    Camera main;
    
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    { 
      
       
    }
}
