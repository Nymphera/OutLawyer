using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
   [SerializeField]
    private GameObject PinBoard;
    public GameState State;
   
    public static event Action<GameState> OnGameStateChanged; 
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {   
        UpdateGameState(GameState.Location);
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
        OfficeManager.Instance.GetComponent<OfficeManager>().enabled = false;


    }

    private void HandleOffice()
    {
        OfficeManager.Instance.GetComponent<OfficeManager>().enabled = true;
    }

}
    public enum GameState
    {
        Office,
        Location
    }
