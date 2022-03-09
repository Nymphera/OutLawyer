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
       
        

    }

    private void GameManager_OnGameStateChanged(GameState State)
    {
        PinBoardButton.SetActive(State==GameState.Office);
        SettingsPanel.SetActive(State==GameState.Office);
    }

    private void OfficeManagerOnStateChanged(OfficeState State)
    {
       
        PinCamera.GetComponent<PinBoardCamera>().enabled = (State == OfficeState.PinBoard);

        //LineButtons.SetActive(State == OfficeState.PinBoard);
        SettingsPanel.SetActive(false);

    }
    private void OnDestroy()
    {
        OfficeManager.OnStateChanged -= OfficeManagerOnStateChanged;
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
    
 



  
   


    
   
    
}
