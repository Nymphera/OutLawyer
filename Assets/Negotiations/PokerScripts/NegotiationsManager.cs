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
        gameControls = new GameControls();
        gameControls.Game.MousePosition.performed += Raycasting;
        gameControls.Game.MouseLeftClick.performed += OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray,out hit, 100))
        {
            if (hit.transform.tag == "Negotiations")
            {
                StartNegotiations();
            }
        }
    }

    private void Raycasting(InputAction.CallbackContext obj)
    {
         mousePosition= gameControls.Game.MousePosition.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        gameControls.Enable();
    }
    
    void StartNegotiations()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Negotiations");
    }
 
}
