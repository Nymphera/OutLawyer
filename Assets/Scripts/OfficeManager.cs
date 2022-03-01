using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OfficeManager : MonoBehaviour
{
    public static event Action <OfficeState> OnStateChanged;
    [SerializeField]
    private GameObject PinBoardCamera,PinBoard;
    

    public OfficeState State;
    private void Awake()
    {
        OfficeManager.OnStateChanged += OfficeManagerOnStateChanged;
        PinBoard.transform.GetChild(1).gameObject.SetActive(false);
    }
    private void OfficeManagerOnStateChanged(OfficeState obj)
    {
      
    }

    void Start()
    {
        UpdateOfficeState(OfficeState.Overview);
    }

    // Update is called once per frame
    void Update()
    {


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
                break;
            case OfficeState.Newspaper:
                break;
            case OfficeState.PinBoard:
                break;
            case OfficeState.Dialogs:
                break;

        }

        //OnStateChanged(newState);
        OnStateChanged?.Invoke(newState);
    }
    } 


public enum OfficeState{
    Overview, //1
    Newspaper,  
    PinBoard,
    Dialogs //>>
    
}
