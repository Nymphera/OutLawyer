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
    private GameObject PinBoardButton, TeleportButton, LineButtons;
    
    private void Awake()
    {
        OfficeManager.OnStateChanged += OfficeManagerOnStateChanged;
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        Instance = this;
        cam = Camera.main;

    }

    private void GameManager_OnGameStateChanged(GameState State)
    {
        PinBoardButton.SetActive(State==GameState.Office);
    }

    private void OfficeManagerOnStateChanged(OfficeState State)
    {
        PinCamera.GetComponent<PinBoardCamera>().enabled = (State == OfficeState.PinBoard);

        LineButtons.SetActive(State == OfficeState.PinBoard);

    }
    private void OnDestroy()
    {
        OfficeManager.OnStateChanged -= OfficeManagerOnStateChanged;
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PointedEvidence=TouchedEvidence();
            PointedEvidence.GetComponentInChildren<Outline>().enabled = true;
        }
        if ( Input.GetMouseButton(1))
        {
            ShowOptions();
          
        
            
        }

    }



    public void SetPlayerLocation()
    {   
        Player = GameObject.Find("Player");
        LocationPosition = new Vector3(0, 0, -15);
        CinemachineSwitcher.Instance.SwitchState();
        Player.transform.position = LocationPosition;
        
        OfficeManager.Instance.UpdateOfficeState(OfficeState.MovingtoLocation);
    }
    public void ShowOptions()
    {
       
            PointedEvidence = TouchedEvidence();
        Evidence Evidence = PointedEvidence.GetComponent<EvidenceDisplay>().Evidence;

    }
    
    private GameObject TouchedEvidence()
    {
        Ray Ray = cam.ScreenPointToRay(Input.mousePosition);
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
    
}
