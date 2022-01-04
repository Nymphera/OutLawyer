using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragPicture : MonoBehaviour, IDragHandler,IPointerDownHandler,IBeginDragHandler,IEndDragHandler


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
         LastPosition=Evidence.anchoredPosition;
        print(LastPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.Mouse0))
            if (IsInBorder(PinBoard, Evidence))
            {
                Evidence.anchoredPosition += eventData.delta;
            }
            else if (IsInBorder(PinBoard, Evidence) == false)
                Evidence.anchoredPosition = LastPosition; 

    }

    public void OnEndDrag(PointerEventData eventData)
    {// Je�li zdj�cie wyjdzie poza granice to restartuje jego pozycj�
        { }
            
        
        
      
    }

    public void OnPointerDown(PointerEventData eventData)
    { //przerzuca zdj�ci� na g�r� stosu, �eby je by�o wida�
        Evidence.SetAsLastSibling();
    }
    
    private bool IsInBorder( RectTransform PinBoard, RectTransform Evidence)
    {/*
      BUG: Na kraw�dziach zdj�cia wariuj� i mo�na zbugowa� jak si� d�ugo trzyma przycisk, albo zmienia rozdizelczo�� na 4k
      */
       Vector3 UpperLimit= ReturnCorners(PinBoard,2);
        Vector3 LowerLimit = ReturnCorners(PinBoard, 0);
        Vector3 EvidenceLowerLimit = ReturnCorners(Evidence, 0);
        Vector3 EvidenceUpperLimit = ReturnCorners(Evidence, 2);
        Vector3 EvidencePosition = Evidence.position;
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

