using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPicture : MonoBehaviour, IDragHandler


//Zdjêcie z tym skryptem bêdzie reagowaæ na przesuniêcie myszy.

{
    [SerializeField]
    private RectTransform PictureToDrag;
   
    public void OnDrag(PointerEventData eventData)
    {   //Pozycja zdjêcie zmienia siê o pozycjê myszki
        PictureToDrag.anchoredPosition += eventData.delta;
    }

   
}

