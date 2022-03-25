using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class PinBoardLogic : MonoBehaviour
{
    public static event Action<Line> OnLineCreated,OnLineDeleted;
    
    private PinBoardControls PinBoardControls;
    private Camera Cam;
   
    [SerializeField]
    private Text Description;
    [SerializeField]
    private Button TeleportButton;
    public static PinBoardLogic Instance;
    private Line Line;
    [SerializeField]
    private Transform LineParent;
    [SerializeField]
    private GameObject linePrefab,SettingsPanel;
    [SerializeField]
    public Vector3[] points;
    [SerializeField] Transform[] Evidences;

    [SerializeField]
    private List<Line> lines = new List<Line>();
    private void Awake()
    {
        points = new Vector3[2];
        Evidences = new Transform[2];

        LineParent = GameObject.Find("LineHolder").transform;
        SettingsPanel = GameObject.Find("SettingsPanel");
        Description = GameObject.Find("Description").GetComponent<Text>();
        TeleportButton = GameObject.Find("Teleport").GetComponent<Button>();

        
        
         PinBoardControls = new PinBoardControls();

        PinBoardControls.PinBoard.MouseLeftClick.performed += MouseLeftClick_performed;
        PinBoardControls.PinBoard.MouseRightClick.performed += MouseRightClick_performed;
        PinBoardControls.PinBoard.DeleteLine.performed += DeleteLine_performed;

        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;

        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SettingsPanel.SetActive(false);
    }
    private void OnDestroy()
    {
        PinBoardControls.PinBoard.MouseLeftClick.performed -= MouseLeftClick_performed;
        PinBoardControls.PinBoard.MouseRightClick.performed -= MouseRightClick_performed;
        PinBoardControls.PinBoard.DeleteLine.performed -= DeleteLine_performed;

        CinemachineSwitcher.OnOfficeStateChanged -= CinemachineSwitcher_OnOfficeStateChanged;
    }
    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
       
        if (state != OfficeState.PinBoard)
        {
            ClearOutline();
            ClearPointsEvidences();
        }

    }

    private void OnEnable()
    {
        PinBoardControls.Enable();
       
    }
    private void DeleteLine_performed(InputAction.CallbackContext obj)
    {
        int childcount = LineParent.childCount;
        OnLineDeleted(LineParent.GetChild(childcount - 1).GetComponent<Line>());
        Destroy(LineParent.GetChild(childcount-1).gameObject);

       

    }

    
   
    private void MouseLeftClick_performed(InputAction.CallbackContext context)
    {
        

        Vector2 pos = Input.mousePosition;
        GameObject Object = TouchedObject(pos);
        
        SettingsPanel.SetActive(false);
        if (Object?.layer == 7)
        {
            
            GameObject Evidence = Object.transform.parent.gameObject;
            
            
            SetPoints(Evidence.transform);
            SetEvidences(Evidence.transform);
        }
     
        

    }

    private void MouseRightClick_performed(InputAction.CallbackContext obj)
    {

        
        Vector2 pos = Input.mousePosition;

        GameObject Object = TouchedObject(pos);
        

        if (Object.layer == 7) //jeœli obiekt to dowód z tablicy
        {

            ShowOptions(Object, pos);

        }
       
        


    }

    private void CreateLine(string color)
    {
        Evidence Evidence0 = Evidences[0].GetComponent<EvidenceDisplay>().Evidence;
        Evidence Evidence1 = Evidences[1].GetComponent<EvidenceDisplay>().Evidence;

        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();

        Line.firstEvidence = Evidence0;
        Line.secondEvidence = Evidence1;

        foreach (Vector3 vector in points)
        {
            Line.AddPoint(vector);
        }


        if (color == "Yellow")
        {
            Line.SetColor(color);
            Line.conectionType = ConectionType.Yellow;
        }
            
        else
             if (color == "Green")
        {
            Line.SetColor(color);
            Line.conectionType = ConectionType.Green;
        }
            
        else
             if (color == "Red")
        {
            Line.SetColor(color);
            Line.conectionType = ConectionType.Red;
        }
            
        else
             if (color == "Blue")
        {
            Line.SetColor(color);
            Line.conectionType = ConectionType.Blue;
        }
            


        OnLineCreated(Line);
        ClearOutline();
        ClearPointsEvidences();

        /*if (lines.Count == 0)
        {
            lines.Add(Line);
            OnLineCreated(Line);
            
        }
            
        else
        {
            bool isInTable=false;
            for (int i = 0; i < lines.Count; i++)    //sprawdza czy stworzona linia jest ju¿ na liœcie linii
            {
                if ((lines[i].firstEvidence == Line.firstEvidence && lines[i].secondEvidence == Line.secondEvidence) || (lines[i].firstEvidence == Line.secondEvidence && lines[i].secondEvidence == Line.firstEvidence))
                {
                    isInTable = true;
                    Debug.Log("You already created such line");
                    Destroy(Line.gameObject);
                }
                
            }
            if (!isInTable)
            {
                lines.Add(Line);
                OnLineCreated(Line);
                
            */
    }
 
    public void CreateLine_Yellow()
    {
        int conectLength = Evidences[1].GetComponent<EvidenceDisplay>().Evidence.Conections.Length;

        Evidence Evidence0 = Evidences[0].GetComponent<EvidenceDisplay>().Evidence;
        Evidence Evidence1 = Evidences[1].GetComponent<EvidenceDisplay>().Evidence;
        for (int i = 0; i < conectLength; i++)
        {
            if (Evidence0 == Evidence1.Conections[i].conected)
            {
                CreateLine("Yellow");
            }
        }
    }

    public void CreateLine_Green()
    {
        int conectLength = Evidences[1].GetComponent<EvidenceDisplay>().Evidence.Conections.Length;

        Evidence Evidence0 = Evidences[0].GetComponent<EvidenceDisplay>().Evidence;
        Evidence Evidence1 = Evidences[1].GetComponent<EvidenceDisplay>().Evidence;
        for (int i = 0; i < conectLength; i++)
        {
            if (Evidence0 == Evidence1.Conections[i].conected)
            {
                CreateLine("Green");
            }
        }
    }
    public void CreateLine_Red()
    {
        int conectLength = Evidences[1].GetComponent<EvidenceDisplay>().Evidence.Conections.Length;
        
        Evidence Evidence0 = Evidences[0].GetComponent<EvidenceDisplay>().Evidence;
        Evidence Evidence1 = Evidences[1].GetComponent<EvidenceDisplay>().Evidence;
        for (int i = 0; i < conectLength; i++)
        {
            if (Evidence0 == Evidence1.Conections[i].conected)
            {
                    CreateLine("Red");
            }
        }
    }
    public void CreateLine_Blue()
    {
        int conectLength = Evidences[1].GetComponent<EvidenceDisplay>().Evidence.Conections.Length;

        Evidence Evidence0 = Evidences[0].GetComponent<EvidenceDisplay>().Evidence;
        Evidence Evidence1 = Evidences[1].GetComponent<EvidenceDisplay>().Evidence;
        for (int i = 0; i < conectLength; i++)
        {
            if (Evidence0 == Evidence1.Conections[i].conected)
            {
                CreateLine("Red");
            }
        }
    }

    private GameObject TouchedObject(Vector2 mouseposition)
    {
        Cam = Camera.main;
        Ray Ray = Cam.ScreenPointToRay(mouseposition);
        RaycastHit Hit;


        if (Physics.Raycast(Ray, out Hit, 1000))
        {

            return Hit.transform.gameObject;
        }
        else return null;

    }
    private void ShowOptions(GameObject Object, Vector2 MousePosition)
    {
        bool ButtonState = false;

        Evidence Evid = Object.transform.GetComponentInParent<EvidenceDisplay>().Evidence;
        if (Evid.evidenceType == Evidence.EvidenceType.Location)
        {
            ButtonState = true;
        }
        Description.text = Evid.Description.ToString();
        SettingsPanel.SetActive(true);
        SettingsPanel.transform.position = MousePosition;
        TeleportButton.gameObject.SetActive(ButtonState);


    }
    private void SetEvidences(Transform Object)     //pobiera Transform dowodu, ustala
    {
        if (Evidences[0] != null)
            Evidences[0].GetChild(1).gameObject.GetComponent<Outline>().enabled = false;    // na bie¿¹co usuwa outline z nieu¿ywanych dowodów

        if (Evidences[1] != null && Evidences[1] != Object)
        {
            Evidences[0] = Evidences[1];
            Evidences[1] = Object;
        }
        else if (Evidences[1] == Object)
        {
            Evidences[0] = Evidences[1];
            Evidences[1] = null;
        }
        else
            Evidences[1] = Object;

        MakeOutline();
    }
    public void SetPoints(Transform Object) // pobiera Transform Dowodu, ustala punkty pinezek
    {



        if (Evidences[1] != null && Evidences[1] != Object)
        {
            points[0] = points[1];
            points[1] = Object.GetChild(1).position;

        }
        else if (Evidences[1] == Object)
        {
            points[0] = points[1];
            points[1] = Vector3.zero;
        }
        else
            points[1] = Object.GetChild(1).position;

    }
    private void ClearPointsEvidences()
    {
        Evidences[0] = null;
        Evidences[1] = null;
        points[0] = Vector3.zero;
        points[1] = Vector3.zero;
    }
    private void MakeOutline()
    {
        foreach (Transform obj in Evidences)
        {
            if (obj != null)
            {
                Transform pin = obj.GetChild(1);
                pin.gameObject.GetComponent<Outline>().enabled = true;
            }

        }
    }
    private void ClearOutline()
    {
        foreach (Transform obj in Evidences)
        {
            if (obj != null)
            {
                Transform pin = obj.GetChild(1);
                pin.gameObject.GetComponent<Outline>().enabled = false;
            }

        }
    }
}

