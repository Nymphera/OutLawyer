using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NegotiationsManager : MonoBehaviour
{
    GameControls gameControls;

    Vector2 mousePosition;
    private void Awake()
    {
        NegotiationsActivator.OnNegotiationsStarted += StartNegotiations;
       
    }
 
    void StartNegotiations()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Negotiations");
    }
 
}
