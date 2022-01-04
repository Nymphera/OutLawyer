using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragPicture : MonoBehaviour, IDragHandler,IPointerDownHandler,IBeginDragHandler


//Zdj�cie z tym skryptem b�dzie reagowa� na przesuni�cie myszy.

{
    [SerializeField]
    private RectTransform Evidence;
    [SerializeField]
    private RectTransform PinBoard;
    private Vector3 velocity = Vector3.zero;
    private Vector3 LastPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {   
        if(IsInBorder(PinBoard,Evidence))
         LastPosition=Evidence.anchoredPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.Mouse0))
            if (IsInBorder(PinBoard, Evidence))
            {//przyporz�dkowuje zdj�ciu pozycj� myszki je�li ta jest na tablicy
                Evidence.anchoredPosition += eventData.delta;
            }
        //zdj�cie wraca do ostatniej pozycji z warto�ci� true
            else if (IsInBorder(PinBoard, Evidence) == false)
                Evidence.anchoredPosition = LastPosition; 

    }

    public void OnPointerDown(PointerEventData eventData)
    { //przerzuca zdj�ci� na g�r� stosu, �eby je by�o wida�
        Evidence.SetAsLastSibling();
    }
    
    private bool IsInBorder( RectTransform PinBoard, RectTransform Evidence)
    {//sprawdza czy Zdj�cie le�y na Pinboardzie
       Vector3 UpperLimit= ReturnCorners(PinBoard,2);
        Vector3 LowerLimit = ReturnCorners(PinBoard, 0);
        Vector3 EvidenceLowerLimit = ReturnCorners(Evidence, 0);
        Vector3 EvidenceUpperLimit = ReturnCorners(Evidence, 2);
       
        if (EvidenceLowerLimit.x >=LowerLimit.x && EvidenceLowerLimit.y >= LowerLimit.y && EvidenceUpperLimit.x <= UpperLimit.x && EvidenceUpperLimit.y <= UpperLimit.y)
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

