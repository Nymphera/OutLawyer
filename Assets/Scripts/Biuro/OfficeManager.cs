using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OfficeManager : MonoBehaviour
{
    private GameObject PinBoard,GameManager, PinBoardUI;
    
    private PinBoardLogic pinBoardLogic;
    private Interact interact;
    

    private InputAction MousePosition;
    private PinBoardControls PinBoardControls;
    private void Awake()
    {
        PinBoardControls = new PinBoardControls();

        PinBoardUI = GameObject.Find("PinBoardCanvas");
        PinBoard = GameObject.Find("PinBoard");
        GameManager = GameObject.Find("GameManager");
        
        pinBoardLogic = PinBoard.GetComponent<PinBoardLogic>();
        interact = GameManager.GetComponent<Interact>();

        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
        PinBoardControls.PinBoard.MouseLeftClick.performed += MouseLeftClick_performed;
        PinBoardControls.PinBoard.LeavePinBoard.performed += LeavePinBoard_performed;

        

    }
    private void Start()
    {
        PinBoardUI.SetActive(false);
        
    }
    private void LeavePinBoard_performed(InputAction.CallbackContext obj)
    {
        CinemachineSwitcher.Instance.SwitchState("Biuro");
    }

    private void OnEnable()
    {
        PinBoardControls.Enable();
        MousePosition = PinBoardControls.PinBoard.Move;
        MousePosition.Enable();
    }
    private void OnDisable()
    {
        PinBoardControls.Disable();
        MousePosition.Disable();
    }
    private void MouseLeftClick_performed(InputAction.CallbackContext obj)
    {
        Ray Ray = Camera.main.ScreenPointToRay(MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if(Physics.Raycast(Ray,out hit))
        {
            if (hit.transform.tag == "Interact")
            {
                CinemachineSwitcher.Instance.SwitchState(hit.transform.name);
                
                    hit.transform.GetComponent<Outline>().enabled = false;
                
            }
        }
    }

    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
        
        pinBoardLogic.enabled = (state == OfficeState.PinBoard);
        interact.enabled = (state == OfficeState.Overview);
        PinBoardUI.SetActive(state == OfficeState.PinBoard);
        
    }

}
