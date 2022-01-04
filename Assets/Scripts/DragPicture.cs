using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragPicture : MonoBehaviour, IDragHandler,IPointerDownHandler


//Zdj�cie z tym skryptem b�dzie reagowa� na przesuni�cie myszy.

{
    [SerializeField]
    private RectTransform Evidence;
    [SerializeField]
    private RectTransform PinBoard;
  
    public void OnDrag(PointerEventData eventData)
    { if(Input.GetKey(KeyCode.Mouse0))
        if (IsInBorder(PinBoard,Evidence))
        {
            Evidence.anchoredPosition += eventData.delta;
        }
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

