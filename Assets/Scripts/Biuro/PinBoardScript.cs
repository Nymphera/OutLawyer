using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class PinBoardScript : MonoBehaviour
{

    
    private CinemachineVirtualCamera PinCamera;
    [SerializeField]
    private GameObject PinBoardButton,  LineButtons,SettingsPanel;
    private PinBoardLogic PinBoardLogic;
   
    private void Awake()
    {
        PinCamera = GameObject.Find("PinBoard Cam").GetComponent<CinemachineVirtualCamera>();
        PinBoardButton = GameObject.Find("PinBoard Button");

        SettingsPanel = GameObject.Find("SettingsPanel");
        PinBoardLogic = GetComponent<PinBoardLogic>();
        OfficeManager.OnStateChanged += OfficeManagerOnStateChanged;
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
       
        

    }

    private void GameManager_OnGameStateChanged(GameState State)
    {
        PinBoardButton.SetActive(State==GameState.Office);
        SettingsPanel.SetActive(State==GameState.Office);
    }

    private void OfficeManagerOnStateChanged(OfficeState State)
    {
       
        PinCamera.GetComponent<PinBoardCamera>().enabled = (State == OfficeState.PinBoard);
        PinBoardLogic.enabled = (State == OfficeState.PinBoard);
        //LineButtons.SetActive(State == OfficeState.PinBoard);
        SettingsPanel.SetActive(false);

    }
    private void OnDestroy()
    {
        OfficeManager.OnStateChanged -= OfficeManagerOnStateChanged;
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
    
 



  
   


    
   
    
}
