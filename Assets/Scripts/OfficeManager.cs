using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OfficeManager : MonoBehaviour
{
    public static event Action <OfficeState> OnStateChanged;
    
    public static OfficeManager Instance;
    private GameObject PinBoard;

    public OfficeState State;
    private void Awake()
    {
        Instance = this;
   
        PinBoard = transform.GetChild(2).gameObject;

    }
 

    void Start()
    {
        UpdateOfficeState(OfficeState.Overview);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(State);

    }

   /* internal static CinemachineSwitcher OnStateChanged()
    {
        throw new NotImplementedException();
    }*/

    public void UpdateOfficeState(OfficeState newState)
    {
        State = newState;
        switch (newState)
        {
            case OfficeState.Overview:
                HandleOverview();
                break;
            case OfficeState.Newspaper:
                HandleNewspaper();      
                break;
            case OfficeState.PinBoard:
                HandlePinBoard();
                break;
            case OfficeState.Dialogs:
                break;

        }

        //OnStateChanged(newState);
        OnStateChanged?.Invoke(newState);
    }

    private void HandlePinBoard()
    {
     
    }

    private void HandleNewspaper()
    {
       
    }

    private void HandleOverview()
    {
      
    }
} 


public enum OfficeState{
    Overview, //1
    Newspaper,  
    PinBoard,
    Dialogs //>>
    
}
