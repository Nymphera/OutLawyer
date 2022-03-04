using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class LineLogic : MonoBehaviour
{
    private LineRenderer LineRenderer;
    private PinBoardScript PinBoardScript;
    private GameObject Evidence;
    private Vector3 Start, End;
    private InputAction MousePosition;
    private PinBoardControls PinBoardControls;
    private Camera Cam;
    private void Awake()
    {
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
        Debug.Log(MousePosition.ReadValue<Vector2>());

        
    }
    private void MouseRightClick_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Right Click");
        Evidence = TouchedEvidence();
        if (Evidence != null)
        {
            Debug.Log("you clicked evidence");
        }

    }

  
    private void OnDisable()
    {
        PinBoardControls.Disable();
    }
  
    private GameObject TouchedEvidence()
    {
        Cam = Camera.main;
        Ray Ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        GameObject Evidence;

        if (Physics.Raycast(Ray, out Hit, 1000))
        {
            if (Hit.transform.gameObject.layer == 7)
            {
                Evidence = Hit.transform.gameObject;
                Debug.Log(Evidence);

                return Evidence;

            }
            else return null;
        }
        else return null;

    }
    private void ShowOptions()
    {

        
        Evidence Evid = Evidence.GetComponent<EvidenceDisplay>().Evidence;
        Debug.Log(Evid.name);
    }
}
