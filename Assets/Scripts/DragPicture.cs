using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPicture : MonoBehaviour, IDragHandler


//Zdj�cie z tym skryptem b�dzie reagowa� na przesuni�cie myszy.

{
    [SerializeField]
    private RectTransform PictureToDrag;
   
    public void OnDrag(PointerEventData eventData)
    {   //Pozycja zdj�cie zmienia si� o pozycj� myszki
        PictureToDrag.anchoredPosition += eventData.delta;
    }

   
}

