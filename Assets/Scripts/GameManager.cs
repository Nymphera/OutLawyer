using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
   
    private GameObject PinBoard;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged; 
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PinBoard = GameObject.Find("PinBoard");
        // PinBoard.SetActive(false);
        // UpdateGameState(GameState.PlayerMove);
        UpdateGameState(GameState.Office);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Office:
                HandleOffice();
                break;
            case GameState.Location:
                HandleLocation();
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleLocation()
    {
        OfficeManager.Instance.gameObject.SetActive(false);
 
    }

    private void HandleOffice()
    {
        OfficeManager.Instance.gameObject.SetActive(true);
    }
    /*   public void OpenPinBoard(bool isOpen)
  {
      if(isOpen==false)
      {
       PinBoard.SetActive(true);

      }
      else //isOpen==true
      {
          PinBoard.SetActive(false);
      }
  }*/

}
    public enum GameState
    {
        Office,
        Location
    }
