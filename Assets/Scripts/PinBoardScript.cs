using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class PinBoardScript : MonoBehaviour
{


   
    private Camera cam;
   
    private Vector3 LocationPosition;

    public static PinBoardScript Instance;
    private GameObject Player, PointedEvidence;

    [SerializeField]
    private CinemachineVirtualCamera PinCamera;
    [SerializeField]
    private GameObject PinBoardButton,  LineButtons,SettingsPanel;
    bool state;
    private void Awake()
    {
         state=true;
        OfficeManager.OnStateChanged += OfficeManagerOnStateChanged;
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        Instance = this;
        cam = Camera.main;

    }

    private void GameManager_OnGameStateChanged(GameState State)
    {
        PinBoardButton.SetActive(State==GameState.Office);
        SettingsPanel.SetActive(false);
    }

    private void OfficeManagerOnStateChanged(OfficeState State)
    {
        PinCamera.GetComponent<PinBoardCamera>().enabled = (State == OfficeState.PinBoard);

        LineButtons.SetActive(State == OfficeState.PinBoard);
        SettingsPanel.SetActive(false);

    }
    private void OnDestroy()
    {
        OfficeManager.OnStateChanged -= OfficeManagerOnStateChanged;
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
    
    private void Update()
    {
       /* if (Input.GetMouseButton(0))
        {
           
            PointedEvidence=TouchedEvidence();
            if(state) 
            {
                PointedEvidence.GetComponent<Outline>().enabled = true;
               
            }          
            else
            {
                PointedEvidence.GetComponent<Outline>().enabled = false;
                
            }
            state = !state;
        }
        if ( Input.GetMouseButton(1))
        {
            ShowOptions();
          
        
            
        }*/

    }



    public void SetPlayerLocation()
    {   
        Player = GameObject.Find("Player");
        LocationPosition = new Vector3(0, 0, -15);
        CinemachineSwitcher.Instance.SwitchState();
        Player.transform.position = LocationPosition;
        
        OfficeManager.Instance.UpdateOfficeState(OfficeState.MovingtoLocation);
    }
   


    
   
    
}
