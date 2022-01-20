using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    LineRenderer Linerenderer;

    private int LayerMask;
    Material Reason  ;   //motyw
    Material Conclusion;    //wniosek
    Material Contradiction; //sprzecznoœæ
    Material Proof ;    //Dowód

    Vector3 MousePosition;

    void Start()
    {
        Reason.SetColor(0, Color.green);
        Conclusion.SetColor(0, Color.blue);
        Contradiction.SetColor(0, Color.red);
        Proof.SetColor(0, Color.yellow);
       


    }
    private void Update()
    {
        MousePosition = Input.mousePosition;
        Ray Ray = Camera.main.ScreenPointToRay(MousePosition);
    }
    private void DrawLine()
    {

    }
}
