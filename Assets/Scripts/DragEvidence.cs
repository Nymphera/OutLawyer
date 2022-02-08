using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEvidence : MonoBehaviour
{
    private Vector3 mouseOffset;
    private Vector3 mouseOffset2;

    private float mouseZCoord;
    private float mouseZCoord2;
    public GameObject Evidence;
    public GameObject Pin;


    void OnMouseDown()

    {

        mouseZCoord = Camera.main.WorldToScreenPoint(Evidence.transform.position).z;
        mouseZCoord2 = Camera.main.WorldToScreenPoint(Pin.transform.position).z;


        mouseOffset = Evidence.transform.position - GetMouseAsWorldPoint();
        mouseOffset2 = Pin.transform.position - GetMouseAsWorldPoint();

    }

    private Vector3 GetMouseAsWorldPoint()

    {

        Vector3 mousePoint = Input.mousePosition;


        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()

    {
      Evidence.transform.position = GetMouseAsWorldPoint() + mouseOffset;
       Pin.transform.position = GetMouseAsWorldPoint() + mouseOffset2;
    }
}
