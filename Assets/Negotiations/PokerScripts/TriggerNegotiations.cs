using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNegotiations : MonoBehaviour
{
    private bool wasPlayed = false;
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.transform.gameObject == gameObject&&GameManager.Instance.CurrentState==GameState.Move)
            {
                if (!wasPlayed)
                {
                    GameEvents.current.TriggerNegotiations();
                    wasPlayed = true;
                }
                
            }
        }
    }

    
}
