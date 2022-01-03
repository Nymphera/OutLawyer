using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPicture : MonoBehaviour, IDragHandler,IPointerDownHandler


//Zdjêcie z tym skryptem bêdzie reagowaæ na przesuniêcie myszy.

{
    [SerializeField]
    private RectTransform PictureToDrag;
   
    public void OnDrag(PointerEventData eventData)
    {   /*
         Pozycja zdjêcie zmienia siê o pozycjê myszki
        Minimalne ró¿nice w przeci¹ganiu myszk¹, delikatnie myszka rozje¿d¿a siê ze zdjêciem
         Na necie znalaz³em jakiœ poradnik z gotowym kodem, ale nie chce mi siê tego teraz sprawdzaæ wiêc sobie zostawie tu link jakby
        nam jednak przeszkadza³o to przesuwanie
        https://www.youtube.com/watch?v=Mb2oua3FjZg
         */
        PictureToDrag.anchoredPosition += eventData.delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    { //przerzuca zdjêciê na górê, ¿eby je by³o widaæ
        PictureToDrag.SetAsLastSibling();
    }
}

