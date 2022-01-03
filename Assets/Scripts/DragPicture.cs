using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPicture : MonoBehaviour, IDragHandler,IPointerDownHandler


//Zdj�cie z tym skryptem b�dzie reagowa� na przesuni�cie myszy.

{
    [SerializeField]
    private RectTransform PictureToDrag;
   
    public void OnDrag(PointerEventData eventData)
    {   /*
         Pozycja zdj�cie zmienia si� o pozycj� myszki
        Minimalne r�nice w przeci�ganiu myszk�, delikatnie myszka rozje�d�a si� ze zdj�ciem
         Na necie znalaz�em jaki� poradnik z gotowym kodem, ale nie chce mi si� tego teraz sprawdza� wi�c sobie zostawie tu link jakby
        nam jednak przeszkadza�o to przesuwanie
        https://www.youtube.com/watch?v=Mb2oua3FjZg
         */
        PictureToDrag.anchoredPosition += eventData.delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    { //przerzuca zdj�ci� na g�r�, �eby je by�o wida�
        PictureToDrag.SetAsLastSibling();
    }
}

