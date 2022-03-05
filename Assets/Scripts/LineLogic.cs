using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class LineLogic : MonoBehaviour
{
    private LineRenderer LineRenderer;
    private PinBoardScript PinBoardScript;
    private GameObject Object;
    private Vector3 Start, End;
    private InputAction MousePosition;
    private PinBoardControls PinBoardControls;
    private Camera Cam;
    [SerializeField]
    private Image SettingsPanel;
    [SerializeField]
    private Text Description;
    
    private Button TeleportButton;

    private void Awake()
    {
        SettingsPanel.gameObject.SetActive(false);
        PinBoardControls = new PinBoardControls();
        LineRenderer = GetComponent<LineRenderer>();
        PinBoardScript = GetComponent<PinBoardScript>();
        PinBoardControls.PinBoard.MouseLeftClick.performed += MouseLeftClick_performed;
        PinBoardControls.PinBoard.MouseRightClick.performed += MouseRightClick_performed;
        
    }
    private void OnEnable()
    {
        PinBoardControls.Enable();
        MousePosition = PinBoardControls.PinBoard.Move;
        MousePosition.Enable();
    }
    
    private void MouseLeftClick_performed(InputAction.CallbackContext obj)
    {
        SettingsPanel.gameObject.SetActive(false);
        Vector2 pos=MousePosition.ReadValue<Vector2>();
        Object = TouchedObject(pos);
        if (Object.transform.gameObject.layer == 8)
        {

            //zmiana koloru linii

        }
        
        
        
        if (Object.layer==7) //jeœli obiekt to dowód z tablicy
        {
            //tworzenie linii z pin position do pozycji myszki
           Vector3 position = GetPinPosition(Object);
            CreateLine(position);
        }
        

    }
    private  void MouseRightClick_performed(InputAction.CallbackContext obj)
    {
        
        Vector2 pos = MousePosition.ReadValue<Vector2>();


        Object = TouchedObject(pos);
         if (Object.layer==7) //jeœli obiekt to dowód z tablicy
         {
            
            ShowOptions(pos);

        }
       
    }


    private void OnDisable()
    {
        PinBoardControls.Disable();
    }


    private GameObject TouchedObject(Vector2 mouseposition)
    {
        Cam = Camera.main;
        Ray Ray = Cam.ScreenPointToRay(mouseposition);
        RaycastHit Hit;
        

        if (Physics.Raycast(Ray, out Hit, 100))
        {
             return  Hit.transform.gameObject;              
        }
        else return null;

    }
    private void ShowOptions(Vector2 MousePosition)
    {
        bool ButtonState=false;
        Object = TouchedObject(MousePosition);
        Evidence Evid = Object.transform.GetComponentInParent<EvidenceDisplay>().Evidence;
        if (Evid.evidenceType == Evidence.EvidenceType.Location)
        {
            ButtonState = true;
        }
        Description.text = Evid.Description.ToString();
        SettingsPanel.gameObject.SetActive(true);
        SettingsPanel.transform.position = MousePosition;
        SettingsPanel.transform.GetChild(1).gameObject.SetActive(ButtonState);


    }
    private Vector3 GetPinPosition(GameObject Object)
    {
        var obj=Object.transform.parent;
        
        if (Object.layer == 7)
        {
            Vector3 PinPosition = obj.GetChild(1).transform.position;



            Debug.Log(PinPosition);
            return PinPosition;
        }
        return Vector3.zero;
    }
    private void DragLineCursor(GameObject Object)
    {
        Vector3 pos = Object.transform.position;
        Object.transform.position = MousePosition.ReadValue<Vector2>();
    }
    private void CreateLine(Vector3 pos)
    { Vector2 mouse = MousePosition.ReadValue<Vector2>();
        Vector3 idontknow = new Vector3(mouse.x, mouse.y, pos.z);
        LineRenderer.SetPosition(0, pos);
        LineRenderer.SetPosition(1, idontknow);
    }
}
