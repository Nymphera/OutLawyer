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
    private GameControls gameControls;





    private PinBoardControls PinBoardControls;

    private OfficeState currentState;
    private void Awake()
    {
        PinBoardControls = new PinBoardControls();
        gameControls = new GameControls();
        

        PinBoardUI = GameObject.Find("PinBoardCanvas");
        Buttons = PinBoardUI.transform.GetChild(1).gameObject;
        Settings= PinBoardUI.transform.GetChild(0).gameObject;
        PinBoard = GameObject.Find("PinBoard");

        interact = GameObject.Find("InteractManager").GetComponent<Interact>();   
       
        pinBoardLogic = PinBoard.GetComponent<PinBoardLogic>();
        
        
        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
        gameControls.Game.MouseLeftClick.performed += MouseLeftClick_performed;
        gameControls.Game.GoBack.performed += LeavePinBoard_performed;
        
        

    }
    private void OnDestroy()
    {
        CinemachineSwitcher.OnOfficeStateChanged -= CinemachineSwitcher_OnOfficeStateChanged;
        PinBoardControls.PinBoard.MouseLeftClick.performed -= MouseLeftClick_performed;
        PinBoardControls.PinBoard.LeavePinBoard.performed -= LeavePinBoard_performed;
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
        if (currentState == OfficeState.Inspect)
            CinemachineSwitcher.Instance.SwitchState("PinBoardSprite");
        else
                CinemachineSwitcher.Instance.SwitchState("Biuro");
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
            Debug.Log("przenosi do nast�pnej lokacji");
            CinemachineSwitcher.Instance.SwitchState("Biuro");
            GameManager.Instance.UpdateGameState(GameState.Location);
        }
        else
            if (name == "Phone")
        {
            Debug.Log("w��cza system dialog�w (chocia� nie  powinien)");
            CinemachineSwitcher.Instance.SwitchState("Biuro");
            GameManager.Instance.UpdateGameState(GameState.Dialog);
        }
        else if (name == "Newspaper")
        {
            Debug.Log("Teraz powinien w��czy� si� system gazety");
        }

    }

    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
        currentState = state;
        pinBoardLogic.enabled = (state == OfficeState.PinBoard);
        if(Buttons!=null)
        Buttons.SetActive(state == OfficeState.PinBoard);
        if (interact != null) ;
        //interact.enabled= (OfficeState.Overview == state||state==OfficeState.Desk);
        //interact.enabled = (state == OfficeState.Overview);
        if (PinBoardUI != null)
            //Buttons.SetActive(state == OfficeState.PinBoard);
                PinBoardUI.SetActive(state == OfficeState.PinBoard||state==OfficeState.Inspect);


       

    }

}
