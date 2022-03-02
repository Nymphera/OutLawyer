using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OfficeManager : MonoBehaviour
{
    public static event Action <OfficeState> OnStateChanged;
    
    public static OfficeManager Instance;
    private GameObject PinBoard;
    [SerializeField]
    private Text text;

    public OfficeState State;
    private void Awake()
    {
        Instance = this;
   
        PinBoard = transform.GetChild(2).gameObject;
        text.gameObject.SetActive(false);
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
            case OfficeState.MovingtoLocation:
                HandleMovingtoLocation();
                break;

        }

        //OnStateChanged(newState);
        OnStateChanged?.Invoke(newState);
    }

        private async void HandleMovingtoLocation()
        {
        Debug.Log("Teleporting!");
        
        await Task.Delay(2000); //animacja przenoszenia siê do lokacji

        GameManager.Instance.UpdateGameState(GameState.Location);
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

} 


public enum OfficeState{
    Overview, //1
    Newspaper,  
    PinBoard,
    Dialogs,
    MovingtoLocation
    
}
