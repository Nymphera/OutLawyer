using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OfficeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PinBoard, PinBoardUI,Buttons,Settings;
    
    private PinBoardManager pinBoardLogic;
   
    private GameControls gameControls;





    private PinBoardControls PinBoardControls;

    private OfficeState currentState;
    private PinBoardState currentPinBoardState;
    private void Awake()
    {
        PinBoardControls = new PinBoardControls();
        gameControls = new GameControls();
        

        PinBoardUI = GameObject.Find("PinBoardCanvas");
        Buttons = PinBoardUI.transform.GetChild(1).gameObject;
        Settings= PinBoardUI.transform.GetChild(0).gameObject;
        PinBoard = GameObject.Find("PinBoard");

        
       
        pinBoardLogic = PinBoard.GetComponent<PinBoardManager>();
        
        
        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
        gameControls.Game.MouseLeftClick.performed += MouseLeftClick_performed;
        gameControls.Game.GoBack.performed += LeavePinBoard_performed;
        
        

    }
    private void OnDestroy()
    {
        CinemachineSwitcher.OnOfficeStateChanged -= CinemachineSwitcher_OnOfficeStateChanged;
        PinBoardControls.PinBoard.MouseLeftClick.performed -= MouseLeftClick_performed;
       
        gameControls.Game.MouseLeftClick.performed -= MouseLeftClick_performed;
        gameControls.Game.GoBack.performed -= LeavePinBoard_performed;
    }
    private void Start()
    {
        PinBoardUI.SetActive(false);
        
    }
    private void LeavePinBoard_performed(InputAction.CallbackContext obj)
    {
        //if(currentState!=OfficeState.Inspect)
        Settings.SetActive(false);
        
            
        if (PinBoardManager.Instance.currentState == PinBoardState.Neutral)
        {
            GameEvents.current.OfficeClick(0);
        }
        if (PinBoardManager.Instance.currentState == PinBoardState.Inspect)
        {
            GameEvents.current.OfficeClick(1);
        }

    }

    private void OnEnable()
    {
        PinBoardControls.Enable();
        gameControls.Enable();
    }
    private void OnDisable()
    {
        gameControls.Disable();
        PinBoardControls.Disable();
    }
    private void MouseLeftClick_performed(InputAction.CallbackContext obj)
    {
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(Ray,out hit))
        {
            if (hit.transform.tag == "Interact"&&currentState==OfficeState.Overview)
            {
             //   CinemachineSwitcher.Instance.SwitchState(hit.transform.name);
                
            }
            /*if (hit.transform.tag == "Interact2"&&currentState==OfficeState.Desk)
            {
                DeskInteractWith(hit.transform.name);
            }*/
        }
    }

    private void DeskInteractWith(string name)
    {
        if (name == "Globus")
        {
            Debug.Log("przenosi do nastêpnej lokacji");
         //  GameEvents.current.OfficeClick.SwitchState(0);
            GameManager.Instance.UpdateGameState(GameState.Move);
        }
        else
            if (name == "Phone")
        {
            Debug.Log("w³¹cza system dialogów (chocia¿ nie  powinien)");
           // CinemachineSwitcher.Instance.SwitchState("Biuro");
            GameManager.Instance.UpdateGameState(GameState.LockInteract);
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
       
        if (PinBoardUI != null)
            
                PinBoardUI.SetActive(state == OfficeState.PinBoard||state==OfficeState.Inspect);


       

    }

}
