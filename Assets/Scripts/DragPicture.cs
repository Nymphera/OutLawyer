using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragPicture : MonoBehaviour, IDragHandler,IPointerDownHandler


//Zdjêcie z tym skryptem bêdzie reagowaæ na przesuniêcie myszy.

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
    { //przerzuca zdjêciê na górê stosu, ¿eby je by³o widaæ
        PictureToDrag.SetAsLastSibling();
    }
    
    private bool IsInBorder(Vector3 MousePosition, RectTransform PinBoard)
    {/*
      BUG: Na krawêdziach zdjêcia wariuj¹ i mo¿na zbugowaæ jak siê d³ugo trzyma przycisk, albo zmienia rozdizelczoœæ na 4k
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

