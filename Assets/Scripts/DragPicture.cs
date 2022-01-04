using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragPicture : MonoBehaviour, IDragHandler,IPointerDownHandler


//Zdj�cie z tym skryptem b�dzie reagowa� na przesuni�cie myszy.

{
    [SerializeField]
    private RectTransform PictureToDrag;
    [SerializeField]
    private RectTransform PinBoard;
  
    public void OnDrag(PointerEventData eventData)
    { if(Input.GetKey(KeyCode.Mouse0))
        if (IsInBorder(Input.mousePosition, PinBoard))
        {
            PictureToDrag.anchoredPosition += eventData.delta;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    { //przerzuca zdj�ci� na g�r� stosu, �eby je by�o wida�
        PictureToDrag.SetAsLastSibling();
    }
    
    private bool IsInBorder(Vector3 MousePosition, RectTransform PinBoard)
    {/*
      BUG: Na kraw�dziach zdj�cia wariuj� i mo�na zbugowa� jak si� d�ugo trzyma przycisk, albo zmienia rozdizelczo�� na 4k
      */
       Vector3 UpperLimit= ReturnCorners(PinBoard,2);
        Vector3 LowerLimit = ReturnCorners(PinBoard, 0);
        if (MousePosition.x >=LowerLimit.x && MousePosition.y >= LowerLimit.y && MousePosition.x < UpperLimit.x && MousePosition.y < UpperLimit.y)
        {
            return true;
        }   
            else
        {
            return false;
        }
        // ReturnCorners() zwraca Rogi Tablicy Korkowej
        Vector3 ReturnCorners(RectTransform PinBoard, int whichCorner)
        {  
            Vector3[] Corners = new Vector3[4];
            PinBoard.GetWorldCorners(Corners);
            return Corners[whichCorner];
        }

    }
}

