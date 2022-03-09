using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
   [SerializeField]
    private GameObject PinBoard,Player;
    public GameState State;
   
    public static event Action<GameState> OnGameStateChanged; 
    private void Awake()
    {   
        Instance = this;
        UpdateGameState(GameState.Move);
    }
  

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Move:
                break;
            case GameState.Inspect:
                break;
            case GameState.Dialog:
                break;
        }
       
        OnGameStateChanged?.Invoke(newState);
    }



}
    public enum GameState
    {
       Move,
       Inspect,
        Dialog,
        Negotiations,
        Office
    }
