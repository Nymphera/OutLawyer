using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinBoardManager : MonoBehaviour
{
    public static event Action<Line> OnLineCreated, OnLineDeleted;
  
    public static PinBoardManager Instance;
    private GameControls GameControls;
    Vector2 mousePosition;
    [SerializeField]
    Texture2D greenWoolTexture, redWoolTexture, yellowWoolTexture, blueWoolTexture,scissorsTextureOpen, scissorsTextureClosed, neutralTexture;
   [SerializeField]
    public PinBoardState currentState=PinBoardState.Neutral;
    OfficeState currentOfficeState=OfficeState.Overview;
    [SerializeField]
    GameObject[] evidences = new GameObject[2];
    [SerializeField]
    GameObject currentEvidence;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip scissorsClip,stringClip;

   
    [SerializeField]
    private GameObject linePrefab;
    [SerializeField]
    private Transform lineParent;
    Line Line;

    private bool isLineCreated=false;
    private bool isLineOverWhiteLine=false;
    private void Awake()
    {
        Instance = this;
        GameControls = new GameControls();
        GameControls.Game.MousePosition.performed += OnMouseMove;
        GameControls.Game.MouseLeftClick.performed += OnMouseClick;
        GameControls.Game.GoBack.performed += CursorToNeutral;

        
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
        GameControls.Game.GoBack.performed -= CursorToNeutral;

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
    private void OnMouseClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
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
                Debug.Log(context);
                if(context.phase==InputActionPhase.Performed)
                    Cursor.SetCursor(scissorsTextureClosed, Vector2.zero, CursorMode.Auto);
                
                DeleteLine(Hit.transform.parent.gameObject);
            }
            else
            {
                //Inspect evidence?
            }
   
        }
    }
    private void DeleteLine(GameObject lineToDestroy)
    {
        if (lineToDestroy.tag == "ColliderLine")
        {
           
            audioSource.PlayOneShot(scissorsClip);
            
            Cursor.SetCursor(scissorsTextureOpen, Vector2.zero, CursorMode.Auto);
            OnLineDeleted(lineToDestroy.GetComponent<Line>());
            Destroy(lineToDestroy);

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
       
        
        Evidence evidence0 = evidences[0].GetComponent<EvidenceDisplay>().Evidence;
        Evidence evidence1 = evidences[1].GetComponent<EvidenceDisplay>().Evidence;

        Line.secondEvidence = evidence1;
        Vector3 secondPoint = currentEvidence.transform.GetChild(1).position;
        Line.SetPoint(1, secondPoint);

        int length=Line.firstEvidence.Conections.Length;
        int length2 = Line.secondEvidence.Conections.Length;
        for (int i = 0; i < length; i++)
        {
            for(int j = 0; j < length2; j++)
            {
                if (evidence1 == evidence0.Conections[i].conected || evidence0 == evidence1.Conections[j].conected)
                {
                    isLineOverWhiteLine = true;
                }
            }    
        }
        if (!isLineOverWhiteLine)
        {
            Debug.Log("Should be destroyed?");
     
            Destroy(Line.gameObject);
        }
        else
        {
            OnLineCreated(Line);
            audioSource.PlayOneShot(stringClip);
            Line.AddColliderToLine();
            StartCoroutine(Line.AnimateLine());
        }
       
       
        

        evidences[0] = null;
        evidences[1] = null;
        isLineOverWhiteLine = false;
    }
    private void CursorToNeutral(InputAction.CallbackContext obj)
    {
        Cursor.SetCursor(neutralTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.Neutral;
        
    }
    public void CursorToYellow()
    {
        Cursor.SetCursor(yellowWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateYellow;
       
    }

    public void CursorToBlue()
    {
        Cursor.SetCursor(blueWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateBlue;
       
    }

    public void CursorToRed()
    {
        Cursor.SetCursor(redWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateRed;
       
    }

    public void CursorToGreen()
    {
        Cursor.SetCursor(greenWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateGreen;
        
    }
    public void CursorToScisors()
    {
        Cursor.SetCursor(scissorsTextureOpen, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.Delete;
        
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