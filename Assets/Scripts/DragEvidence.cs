using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEvidence : MonoBehaviour
{
    private Vector3 mouseOffset;

    private float mouseZCoord;




    void OnMouseDown()

    {

        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;


        mouseOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }

    private Vector3 GetMouseAsWorldPoint()

    {

        Vector3 mousePoint = Input.mousePosition;


        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()

    {
        transform.position = GetMouseAsWorldPoint() + mouseOffset;
    }
}
