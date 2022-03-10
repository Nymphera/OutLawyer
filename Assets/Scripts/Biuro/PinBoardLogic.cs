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
    public List<Vector3> points = new List<Vector3>();
    [SerializeField]
    Transform[] Pins;
    private void Awake()
    {
        Pins = new Transform[2];
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
        Vector2 pos=MousePosition.ReadValue<Vector2>();
        GameObject Object = TouchedObject(pos);
        GameObject Evidence = Object.transform.parent.gameObject;


        SettingsPanel.gameObject.SetActive(false);

        if (Object.layer==7) 
        {      
             GetPinsPosition(Evidence.transform);
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
    public void GetPinsPosition(Transform Object)
    {    
        Transform Pin = Object.GetChild(1);
     
        if(Pins[0]!=null)
        Pins[0].gameObject.GetComponent<Outline>().enabled = false;
        Pins[0] = Pins[1];
        Pins[1] = Pin;
       
        foreach(Transform obj in Pins)
        { if(obj!=null)
            obj.gameObject.GetComponent<Outline>().enabled = true;
           points.Add(obj.position);
        }      
        
    }

 
    public void CreateLine()
    {
        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
        
        foreach (var vector in points)
        {
            Line.AddPoint(vector);
        }
        //to chyba nie dzia³a
        if (Line.pointsCount != 2)
            Destroy(LineParent.transform.GetChild(Line.pointsCount-1).gameObject);
    }
}
