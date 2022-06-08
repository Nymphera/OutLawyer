using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeActionTrigger : MonoBehaviour
{
    [SerializeField]
    private int objectID;
    
    private void OnMouseDown()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f)&&GameManager.Instance.CurrentState==GameState.Office)
        {
            if (hit.transform.gameObject == gameObject)
            {
                GameEvents.current.OfficeClick(objectID);
               
            }
        }

    }
}
