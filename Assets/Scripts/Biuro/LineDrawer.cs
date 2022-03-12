using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineDrawer : MonoBehaviour
{[SerializeField]
    private Transform Pin1, Pin2;
    private PinBoardControls PinBoardControls;
    InputAction mousepos;
    [SerializeField]
    private GameObject linePrefab;
    
    private Line currentLine;
    private float width = 0.2f;
   

   [Space(30f)]
    private Gradient lineColor;
    private void Awake()
    {  
        PinBoardControls = new PinBoardControls();

    }
    private void OnEnable()
    {
        PinBoardControls.Enable();
        mousepos = PinBoardControls.PinBoard.Move;
        mousepos.Enable();
    }
    private void OnDisable()
    {
        PinBoardControls.Disable();
    }

    public void BeginDraw(Vector3 position)
    {
        currentLine=Instantiate(linePrefab, this.transform).GetComponent<Line>();
       // currentLine.SetColor(lineColor);
        currentLine.SetLineWidth(width);
        currentLine.AddPoint(position);
    }
    public void Draw(Vector3 position)
    {
       
      
        
    }
}
