using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScisors : MonoBehaviour
{
    RectTransform rect;
    private void Update()
    {
        
    }
    public void Drag()
    {
        rect.position = Input.mousePosition;
    }
}
