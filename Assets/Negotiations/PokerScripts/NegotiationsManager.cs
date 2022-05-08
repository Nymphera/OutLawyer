using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NegotiationsManager : MonoBehaviour
{
    [SerializeField]
    GameObject cardPrefab;

    DealCards deal = new DealCards();
    private void Awake()
    {
        NegotiationsActivator.OnNegotiationsStarted += StartNegotiations;
       
    }
 
    void StartNegotiations()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Negotiations");
        deal.Deal();
        
    }
    
}
