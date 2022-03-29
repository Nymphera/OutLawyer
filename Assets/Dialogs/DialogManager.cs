using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Dialog dialog;

    public void OnPointerClick(PointerEventData eventData)
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit, 100))
        {
            Debug.Log(hit);
        }
        Debug.Log("collision");
    }

   
}
