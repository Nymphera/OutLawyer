using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfficeManager : MonoBehaviour
{
    public static event Action <OfficeState> OnStateChanged;
    
    public static OfficeManager Instance;
    private HelpLines HelpLines;

    public OfficeState State;
    private void Awake()
    {
        Instance = this;

        
    }
 

    void Start()
    {
        UpdateOfficeState(OfficeState.Overview);
    }


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
            case OfficeState.MovingtoLocation:
                HandleMovingtoLocation();
                break;

        }


        OnStateChanged?.Invoke(newState);
    }

        private async void HandleMovingtoLocation()
        {
        Debug.Log("Teleporting!");
        
        await Task.Delay(2000);

        SceneManager.LoadScene("Krabiarnia");
        }

    private void HandlePinBoard()
    {
     



    }

    private void HandleNewspaper()
    {
       //show Newspaper
    }

    private void HandleOverview()
    {
     
    }
    public void MoveToOffice()
    {
        GameManager.Instance.UpdateGameState(GameState.Office);
    }
} 


public enum OfficeState{
    Overview, //1
    Newspaper,  
    PinBoard,
    Dialogs,
    MovingtoLocation
    
}
