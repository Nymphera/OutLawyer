using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class PinBoardLogic : MonoBehaviour
{
    
    private InputAction MousePosition;
    private PinBoardControls PinBoardControls;
    private Camera Cam;
    [SerializeField]
    private Image SettingsPanel;
    [SerializeField]
    private Text Description;
    [SerializeField]
    private Button TeleportButton;
    public static PinBoardLogic Instance;
    private Line Line;
    [SerializeField]
    private Transform LineParent;
    [SerializeField]
    private GameObject linePrefab;
    [SerializeField]
    public Vector3[] points;
   [SerializeField] Transform[] Evidences;
    private void Awake()
    {
        Evidences = new Transform[2];
        points = new Vector3[2];
        Instance = this;
        SettingsPanel.gameObject.SetActive(false);
        PinBoardControls = new PinBoardControls();
       
        //PinBoardScript = GetComponent<PinBoardScript>();
       PinBoardControls.PinBoard.MouseLeftClick.performed += MouseLeftClick_performed;
        PinBoardControls.PinBoard.MouseRightClick.performed += MouseRightClick_performed;
        
    }
    private void OnEnable()
    {
        PinBoardControls.Enable();
        MousePosition = PinBoardControls.PinBoard.Move;
        MousePosition.Enable();
    }

    private void MouseLeftClick_performed(InputAction.CallbackContext context)
    {
        Vector2 pos = MousePosition.ReadValue<Vector2>();
        GameObject Object = TouchedObject(pos);
       
            SettingsPanel.gameObject.SetActive(false);
            if (Object?.layer == 7)
            {
                GameObject Evidence = Object.transform.parent.gameObject;
                GetPinPosition(Evidence.transform);
            }

     }  
 
    private void MouseRightClick_performed(InputAction.CallbackContext obj)
    {
        
        Vector2 pos = MousePosition.ReadValue<Vector2>();


        GameObject Object = TouchedObject(pos);
       
        if (Object.layer==7) //jeœli obiekt to dowód z tablicy
         {
            
            ShowOptions(Object,pos);

        }
       
    }


    private void OnDisable()
    {
        PinBoardControls.Disable();
    }


    public GameObject TouchedObject(Vector2 mouseposition)
    {
        Cam = Camera.main;
        Ray Ray = Cam.ScreenPointToRay(mouseposition);
        RaycastHit Hit;
       

        if (Physics.Raycast(Ray, out Hit, 1000))
        {
           
            return  Hit.transform.gameObject;              
        }
        else return null;

    }
    private void ShowOptions(GameObject Object,Vector2 MousePosition)
    {
        bool ButtonState=false;
        
        Evidence Evid = Object.transform.GetComponentInParent<EvidenceDisplay>().Evidence;
        if (Evid.evidenceType == Evidence.EvidenceType.Location)
        {
            ButtonState = true;
        }
        Description.text = Evid.Description.ToString();
        SettingsPanel.gameObject.SetActive(true);
        SettingsPanel.transform.position = MousePosition;
        TeleportButton.gameObject.SetActive(ButtonState);


    }
    public void GetPinPosition(Transform Object) // pobiera Transform Dowodu, mo¿na wzi¹æ Evidence Display
    {    
        Transform Pin = Object.GetChild(1);
     
        if(Evidences[0]!=null)
        Evidences[0].GetChild(1).gameObject.GetComponent<Outline>().enabled = false;
       if (!(Evidences[1] == Object||Evidences[0]==Object))
        {
            
            Evidences[0] = Evidences[1];
            Evidences[1] = Object;

            points[0] = points[1];
            points[1] = Object.GetChild(1).position;
        }
       
       
        foreach(Transform obj in Evidences)
        { if(obj!=null)
            {
                Transform pin = obj.GetChild(1);
                pin.gameObject.GetComponent<Outline>().enabled = true;
            }
            
        }
       
    }

 
    public void CreateLine_Yellow()
    {
      
        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
        Line.SetColor("Yellow");
        Line.AnimateLine();
        foreach (var vector in points)
        {
            Line.AddPoint(vector);
        }
        //to chyba nie dzia³a
        if (Line.pointsCount != 2)
            Destroy(LineParent.transform.GetChild(Line.pointsCount-1).gameObject);
        
    }
    public void CreateLine_Green()
    {

        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
        Line.SetColor("Green");
        Line.AnimateLine();
        foreach (var vector in points)
        {
            Line.AddPoint(vector);
        }
        //to chyba nie dzia³a
        if (Line.pointsCount != 2)
            Destroy(LineParent.transform.GetChild(Line.pointsCount - 1).gameObject);
    }
    public void CreateLine_Red()
    {

        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
        Line.SetColor("Red");
        Line.AnimateLine();
        foreach (var vector in points)
        {
            Line.AddPoint(vector);
        }
        //to chyba nie dzia³a
        if (Line.pointsCount != 2)
            Destroy(LineParent.transform.GetChild(Line.pointsCount - 1).gameObject);
    }
    public void CreateLine_Blue()
    {

        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
        Line.SetColor("Blue");
        Line.AnimateLine();
        foreach (var vector in points)
        {
            Line.AddPoint(vector);
        }
        //to chyba nie dzia³a
        if (Line.pointsCount != 2)
            Destroy(LineParent.transform.GetChild(Line.pointsCount - 1).gameObject);
    }
}

