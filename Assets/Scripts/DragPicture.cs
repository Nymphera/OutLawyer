using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragPicture : MonoBehaviour, IDragHandler,IPointerDownHandler,IEndDragHandler


//Zdjêcie z tym skryptem bêdzie reagowaæ na przesuniêcie myszy.

{
    [SerializeField]
    private RectTransform Evidence;
    [SerializeField]
    private RectTransform PinBoard;
    private void Start()
    {
        Debug.Log("Evidence: " + Evidence.transform.localPosition);
        Debug.Log("Pinboard: " + PinBoard.transform.localPosition);

    }
    private void Update()
    {
        Debug.Log("Evidence: " + Evidence.transform.localPosition);

    }

    public void OnDrag(PointerEventData eventData)
    {
      

        if (Input.GetKey(KeyCode.Mouse0))
        if (IsInBorder(PinBoard,Evidence))
        {
            Evidence.anchoredPosition += eventData.delta;
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {// Jeœli zdjêcie wyjdzie poza granice to restartuje jego pozycjê
        if (IsInBorder(PinBoard, Evidence) == false)
        {
            Evidence.transform.Translate(Vector3.MoveTowards( -Evidence.transform.localPosition, PinBoard.transform.localPosition, 0));
        }
        
      
    }

    public void OnPointerDown(PointerEventData eventData)
    { //przerzuca zdjêciê na górê stosu, ¿eby je by³o widaæ
        Evidence.SetAsLastSibling();
    }
    
    private bool IsInBorder( RectTransform PinBoard, RectTransform Evidence)
    {/*
      BUG: Na krawêdziach zdjêcia wariuj¹ i mo¿na zbugowaæ jak siê d³ugo trzyma przycisk, albo zmienia rozdizelczoœæ na 4k
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

