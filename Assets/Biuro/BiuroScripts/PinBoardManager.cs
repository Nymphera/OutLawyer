using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    AudioClip scissorsClip,stringClip,wrongConectionClip;
    
   
    [SerializeField]
    private GameObject linePrefab;
    [SerializeField]
    private Transform lineParent;
    private Line Line;
    private List<Line> lines = new List<Line>();

    private bool isLineCreated=false;
    private bool isLineOverOtherLine=false;
    private bool isLineOverWhiteLine=false;


    private GameObject RedButton, GreenButton, YellowButton, BlueButton;
    private Text redText, greenText, yellowText, blueText;
    private int redCount = 0, greenCount = 0, yellowCount = 0, blueCount = 0;
    private bool redButtonState, greenButtonState, yellowButtonState, blueButtonState;
    private void Awake()
    {
        Instance = this;
        GameControls = new GameControls();
        GameControls.Game.MousePosition.performed += OnMouseMove;
        GameControls.Game.MouseLeftClick.performed += OnMouseClick;
        GameControls.Game.GoBack.performed += CursorToNeutral;

        RedButton = GameObject.Find("RedButton");
        GreenButton = GameObject.Find("GreenButton");
        BlueButton = GameObject.Find("BlueButton");
        YellowButton = GameObject.Find("YellowButton");

        redText = RedButton.transform.GetChild(0).GetComponent<Text>();
        greenText = GreenButton.transform.GetChild(0).GetComponent<Text>();
        blueText = BlueButton.transform.GetChild(0).GetComponent<Text>();
        yellowText = YellowButton.transform.GetChild(0).GetComponent<Text>();

       
        
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
    private void Start()
    {
        if (GameManager.Instance.createdLines.Count > 0)
           
        CreateLines(GameManager.Instance.createdLines.ToArray());
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
            if (currentState != PinBoardState.Delete && currentState != PinBoardState.Neutral&& currentState!=PinBoardState.Burned)
            {
                //createLine
                
                CreateLine(Hit);
            }
              else if(currentState == PinBoardState.Delete)
            {
                //delete Line

                StartCoroutine(DeleteLine(Hit.transform.parent.gameObject));
            }
            else
            {
                //Inspect evidence?
            }
   
        }
    }
    private void CountLines(Line line,bool isLineCreated)
    {
        redCount = Int32.Parse(redText.text);
        yellowCount = Int32.Parse(yellowText.text);
        blueCount = Int32.Parse(blueText.text);
        greenCount = Int32.Parse(greenText.text);
        if (!isLineCreated)
        {
            if (line.conectionType == ConectionType.Blue)
                blueCount++;
            else
             if (line.conectionType == ConectionType.Red)
                redCount++;
            else
             if (line.conectionType == ConectionType.Green)
                greenCount++;
            else
             if (line.conectionType == ConectionType.Yellow)
                yellowCount++;
        }
        else
        {
            if (line.conectionType == ConectionType.Blue)
                blueCount--;
            else
            if (line.conectionType == ConectionType.Red)
                redCount--;
            else
            if (line.conectionType == ConectionType.Green)
                greenCount--;
            else
            if (line.conectionType == ConectionType.Yellow)
                yellowCount--;
        }
        OverWriteButtons();
        redButtonState = RedButton.GetComponent<Button>().enabled;
        greenButtonState = GreenButton.GetComponent<Button>().enabled;
        yellowButtonState = YellowButton.GetComponent<Button>().enabled;
        blueButtonState = BlueButton.GetComponent<Button>().enabled;

        RedButton.GetComponent<Button>().enabled = (redCount > 0);
        BlueButton.GetComponent<Button>().enabled = (blueCount > 0);
        YellowButton.GetComponent<Button>().enabled = (yellowCount > 0);
        GreenButton.GetComponent<Button>().enabled = (greenCount > 0);

        if (redButtonState != RedButton.GetComponent<Button>().enabled)
        {
            DeactiveButton(RedButton);
        }
        if (greenButtonState != GreenButton.GetComponent<Button>().enabled)
        {
            DeactiveButton(GreenButton);
        }
        if (yellowButtonState != YellowButton.GetComponent<Button>().enabled)
        {
            DeactiveButton(YellowButton);
        }
        if (blueButtonState != BlueButton.GetComponent<Button>().enabled)
        {
            DeactiveButton(BlueButton);
        }
    }

    private void DeactiveButton(GameObject button)
    {
        Color color = button.GetComponent<Image>().color;
        if (color.a > 0.5f)
        {
            color.a = 0.3f;
            //cursor tu neutral
            Cursor.SetCursor(neutralTexture, Vector2.zero, CursorMode.Auto);
            currentState = PinBoardState.Neutral;
        }
        else
        {
            color.a = 1;
        }
        button.GetComponent<Image>().color = color;
    }

    private void OverWriteButtons()
    {
        redText.text = redCount.ToString();
        greenText.text = greenCount.ToString();
        blueText.text = blueCount.ToString();
        yellowText.text = yellowCount.ToString();
    }

    private IEnumerator DeleteLine(GameObject lineToDestroy)
    {
        Line line = lineToDestroy.GetComponent<Line>();

        if (lineToDestroy.tag == "ColliderLine"&&!line.wasLineBurned)
        {
            TriggerLineDeleted(lineToDestroy.GetComponent<Line>());
            Cursor.SetCursor(scissorsTextureClosed, new Vector2(30, 30), CursorMode.Auto);
            audioSource.PlayOneShot(scissorsClip);
            lines.Remove(lineToDestroy.GetComponent<Line>());
            CountLines(lineToDestroy.GetComponent<Line>(), false);
            Destroy(lineToDestroy);

            yield return new WaitForSeconds(0.5f);

            Cursor.SetCursor(scissorsTextureOpen, new Vector2(30, 30), CursorMode.Auto);
            
        }
    }
    private void CreateLines(LineData[] lineData)
    {
        int count = lineData.Length;
        foreach( LineData data in lineData)
        {
           Line line= Instantiate(linePrefab, lineParent).GetComponent<Line>();
            line.firstEvidence = data.firstEvidence;
            line.secondEvidence = data.secondEvidence;
            line.conectionType = data.conectionType;
            line.wasLineBurned = data.wasLineBurned;
            line.isConectionGood = data.isConectionGood;
            if (line.wasLineBurned)
            {
                if (line.isConectionGood)
                    line.conectionType = ConectionType.White;
                else
                    line.conectionType = ConectionType.Black;

            }
            line.SetColor(line.conectionType.ToString());
            
            GameObject obj= GameObject.Find(line.firstEvidence.Name);
            
            Vector3 firstPoint = obj.transform.GetChild(1).position;
           
            line.AddPoint(firstPoint);
            
            GameObject obj2 = GameObject.Find(line.secondEvidence.Name);
           
            Vector3 secondPoint = obj2.transform.GetChild(1).position;
            
            line.AddPoint(secondPoint);
            lines.Add(line);
            line.AddColliderToLine();
            TriggerLineCreated(line);
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
        else if(evidences[0]!= null)
        {
            EndLine();
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

        length = lines.Count;
        for(int i = 0; i < length; i++)
        {
            if((lines[i].firstEvidence==Line.firstEvidence&&lines[i].secondEvidence==Line.secondEvidence)|| (lines[i].secondEvidence == Line.firstEvidence && lines[i].firstEvidence == Line.secondEvidence))
            {
                isLineOverOtherLine = true;
            }
        }

        if (!isLineOverWhiteLine||isLineOverOtherLine)
        {
            
            audioSource.PlayOneShot(wrongConectionClip);
          
            //????
            
            Destroy(Line.gameObject);
        }
        else
        {
            CountLines(Line, true);
            lines.Add(Line);
            TriggerLineCreated(Line);
            audioSource.PlayOneShot(stringClip);
            Line.AddColliderToLine();
            StartCoroutine(Line.AnimateLine());
        }
        if (Line.secondEvidence == null)
            Destroy(Line.gameObject);



        evidences[0] = null;
        evidences[1] = null;
        isLineOverOtherLine = false;
        isLineOverWhiteLine = false;

    }
    public void TriggerLineCreated(Line line)
    {
        OnLineCreated(line);
    }
    public void TriggerLineDeleted(Line line)
    {
        OnLineDeleted(line);
    }
    private void CursorToNeutral(InputAction.CallbackContext obj)
    {
        EndLine();
        Cursor.SetCursor(neutralTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.Neutral;
        
    }
    public void CursorToYellow()
    {
        Cursor.SetCursor(yellowWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateYellow;
        EndLine();
    }

    public void CursorToBlue()
    {
        Cursor.SetCursor(blueWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateBlue;
        EndLine();
    }

    public void CursorToRed()
    {
        Cursor.SetCursor(redWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateRed;
        EndLine();
    }

    public void CursorToGreen()
    {
        Cursor.SetCursor(greenWoolTexture, Vector2.zero, CursorMode.Auto);
        currentState = PinBoardState.CreateGreen;
        EndLine();
    }
    public void CursorToScisors()
    {
        Cursor.SetCursor(scissorsTextureOpen, new Vector2(30,30), CursorMode.Auto);
        currentState = PinBoardState.Delete;
        EndLine();
    }
    public void Burn()
    {
        currentState = PinBoardState.Burned;
        EndLine();
    }
}
public enum PinBoardState
{
    CreateGreen,
    CreateRed,
    CreateBlue,
    CreateYellow,
    Delete,
    Neutral,
    Burned
}