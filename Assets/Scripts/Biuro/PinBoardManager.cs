using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBoardManager : MonoBehaviour
{
    public static event Action<Line> OnLineCreated, OnLineDeleted;
    public static PinBoardManager Instance;
    private GameControls GameControls;
    Vector2 mousePosition;

    Texture2D greenWoolTexture, redWoolTexture, yellowWoolTexture, blueWoolTexture,scissorsTexture;
   [SerializeField]
    PinBoardState currentState=PinBoardState.Neutral;
    [SerializeField]
    GameObject[] evidences = new GameObject[2];
    [SerializeField]
    GameObject currentEvidence;
    
   
    [SerializeField]
    private GameObject linePrefab;
    [SerializeField]
    private Transform lineParent;
    Line Line;

    private bool isLineCreated=false;
    private void Awake()
    {
        Instance = this;
        GameControls = new GameControls();
        GameControls.Game.MousePosition.performed += OnMouseMove;
        GameControls.Game.MouseLeftClick.performed += OnMouseClick;


    }

    
    private void OnEnable()
    {
        GameControls.Enable();
    }
    private void OnDisable()
    {
        GameControls.Disable();
    }
    private void OnDestroy()
    {
        GameControls.Game.MousePosition.performed -= OnMouseMove;
        GameControls.Game.MouseLeftClick.performed -= OnMouseClick;
    }
    private void OnMouseDown()
    {
        
    }
    private void OnMouseMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        if (currentState != PinBoardState.Neutral)
        {
            mousePosition = GameControls.Game.MousePosition.ReadValue<Vector2>();
        }

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, 1000))
        {
           
            if (isLineCreated)
            {
                Vector3 linePositionOnScreen = hitData.point;
                Vector3 linePosition = new Vector3(Line.points[0].x, linePositionOnScreen.y, linePositionOnScreen.z);
                EditLine(Line, linePosition);
            }
            

        }

    }
    private void OnMouseClick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        Ray Ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit Hit;


        if (Physics.Raycast(Ray, out Hit, 1000))
        {
            if (currentState != PinBoardState.Delete && currentState != PinBoardState.Neutral)
            {
                //createLine
                CreateLine(Hit);
            }
              else if(currentState == PinBoardState.Delete)
            {
                //delete Line
                Debug.Log(Hit.transform.name);
                DeleteLine(Hit);
            }
            else
            {
                //Inspect evidence?
            }
   
        }
    }

    private void DeleteLine(RaycastHit Hit)
    {
        Debug.Log("Destroy");
        if (Hit.transform.gameObject.tag == "ColliderLine")
        {
            
            Destroy(Hit.transform.parent.gameObject);
        }
    }

    private void CreateLine(RaycastHit Hit)
    {
        if (Hit.transform.gameObject.layer == 7)
        {
            currentEvidence = Hit.transform.parent.gameObject;
            if (currentEvidence.name == "Pin")
                currentEvidence = currentEvidence.transform.parent.gameObject;
            if (evidences[0] == null)
            {

                StartLine();

            }
            else
            {
                EndLine();
            }
        }
    }

    private void StartLine()
    {
        isLineCreated = true;
        evidences[0] = currentEvidence;
        Debug.Log("CreateLine");
        Line = Instantiate(linePrefab, lineParent).GetComponent<Line>();
        Evidence evidence = currentEvidence.GetComponent<EvidenceDisplay>().Evidence;
        Line.firstEvidence = evidence;
        
        if (currentState == PinBoardState.CreateBlue)
        {
            Line.SetColor("Blue");
        }
        else if(currentState == PinBoardState.CreateRed)
        {
            Line.SetColor("Red");
        }
        else if (currentState == PinBoardState.CreateGreen)
        {
            Line.SetColor("Green");
        }
        else if (currentState == PinBoardState.CreateYellow)
        {
            Line.SetColor("Yellow");
        }
        Vector3 firstPoint = currentEvidence.transform.GetChild(1).position;
        Line.AddPoint(firstPoint);
        // drugi punkt na razie jest w miejscu peirwsszego
        Line.AddPoint(firstPoint);
        
    }
    private void EditLine(Line line, Vector3 position)
    {
        line.SetPoint(1,position);
    }
    private void EndLine()
    {
        isLineCreated = false;
        evidences[1] = currentEvidence;
        OnLineCreated(Line);
        Debug.Log("EndLine");
        Evidence evidence0 = evidences[0].GetComponent<EvidenceDisplay>().Evidence;
        Evidence evidence1 = evidences[1].GetComponent<EvidenceDisplay>().Evidence;

        Line.secondEvidence = evidence1;
        Vector3 secondPoint = currentEvidence.transform.GetChild(1).position;
        Line.SetPoint(1, secondPoint);

        int length=Line.firstEvidence.Conections.Length; 
        for (int i = 0; i < length; i++)
        {
            if (evidence1 == evidence0.Conections[i].conected)
            {
                Line.isConectionGood = true;
                
            }
            else
            {
               // Destroy(Line.gameObject);
                Debug.Log("Should be destroyed?");
            }
                
        }
        Line.AddColliderToLine();
        StartCoroutine(Line.AnimateLine());
        evidences[0] = null;
        evidences[1] = null;
       
    }

    public void CursorToYellow()
    {
        Cursor.SetCursor(yellowWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateYellow;
        Debug.Log("ChangeCursor");
    }

    public void CursorToBlue()
    {
        Cursor.SetCursor(blueWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateBlue;
        Debug.Log("ChangeCursor");
    }

    public void CursorToRed()
    {
        Cursor.SetCursor(redWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateRed;
        Debug.Log("ChangeCursor");
    }

    public void CursorToGreen()
    {
        Cursor.SetCursor(greenWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateGreen;
        Debug.Log("ChangeCursor");
    }
    public void CursorToScisors()
    {
       // Cursor.SetCursor(scissorsTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.Delete;
        Debug.Log("ChangeCursor");
    }
}
public enum PinBoardState
{
    CreateGreen,
    CreateRed,
    CreateBlue,
    CreateYellow,
    Delete,
    Neutral
}