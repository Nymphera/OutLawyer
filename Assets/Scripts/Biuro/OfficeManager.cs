using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OfficeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PinBoard, PinBoardUI,Buttons,Settings;
    
    private PinBoardLogic pinBoardLogic;
    private Interact interact;
    private Interact2 interact2;
    
    

    private InputAction MousePosition;
    private PinBoardControls PinBoardControls;

    private OfficeState currentState;
    private void Awake()
    {
        PinBoardControls = new PinBoardControls();

        

        PinBoardUI = GameObject.Find("PinBoardCanvas");
        Buttons = PinBoardUI.transform.GetChild(1).gameObject;
        Settings= PinBoardUI.transform.GetChild(0).gameObject;
        PinBoard = GameObject.Find("PinBoard");

        interact = GameObject.Find("Interact").GetComponent<Interact>();   
        interact2= GameObject.Find("Interact2").GetComponent<Interact2>();
        pinBoardLogic = PinBoard.GetComponent<PinBoardLogic>();
        

        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
        PinBoardControls.PinBoard.MouseLeftClick.performed += MouseLeftClick_performed;
        PinBoardControls.PinBoard.LeavePinBoard.performed += LeavePinBoard_performed;

        

    }
    private void OnDestroy()
    {
        CinemachineSwitcher.OnOfficeStateChanged -= CinemachineSwitcher_OnOfficeStateChanged;
        PinBoardControls.PinBoard.MouseLeftClick.performed -= MouseLeftClick_performed;
        PinBoardControls.PinBoard.LeavePinBoard.performed -= LeavePinBoard_performed;
    }
    private void Start()
    {
        PinBoardUI.SetActive(false);
        
    }
    private void LeavePinBoard_performed(InputAction.CallbackContext obj)
    {
        //if(currentState!=OfficeState.Inspect)
        Settings.SetActive(false);
        if (currentState == OfficeState.Inspect)
            CinemachineSwitcher.Instance.SwitchState("PinBoardSprite");
        else
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
            if (hit.transform.tag == "Interact"&&currentState==OfficeState.Overview)
            {
                CinemachineSwitcher.Instance.SwitchState(hit.transform.name);
                
            }
            if (hit.transform.tag == "Interact2"&&currentState==OfficeState.Desk)
            {
                DeskInteractWith(hit.transform.name);
            }
        }
    }

    private void DeskInteractWith(string name)
    {
        if (name == "Globus")
        {
            Debug.Log("przenosi do nastêpnej lokacji");
            CinemachineSwitcher.Instance.SwitchState("Biuro");
            GameManager.Instance.UpdateGameState(GameState.Location);
        }
        else
            if (name == "Phone")
        {
            Debug.Log("w³¹cza system dialogów (chocia¿ nie  powinien)");
            CinemachineSwitcher.Instance.SwitchState("Biuro");
            GameManager.Instance.UpdateGameState(GameState.Dialog);
        }
        else if (name == "Newspaper")
        {
            Debug.Log("Teraz powinien w³¹czyæ siê system gazety");
        }

    }

    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
        currentState = state;
        pinBoardLogic.enabled = (state == OfficeState.PinBoard);
        if(Buttons!=null)
        Buttons.SetActive(state == OfficeState.PinBoard);
        if(interact!=null)
        interact.enabled= (OfficeState.Overview == state);
        //interact.enabled = (state == OfficeState.Overview);
        if (PinBoardUI != null)
            //Buttons.SetActive(state == OfficeState.PinBoard);
                PinBoardUI.SetActive(state == OfficeState.PinBoard||state==OfficeState.Inspect);


        if(interact2!=null)
        interact2.enabled = (state == OfficeState.Desk);

    }

}
