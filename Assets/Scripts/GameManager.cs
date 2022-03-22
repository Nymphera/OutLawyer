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

       
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //OnGameStateChanged?.Invoke(GameState.Office);
    }

    

    public void UpdateGameState(GameState newState)
    {
        /* switch dla jakiejœ specjalnej logiki
        switch (newState)
        {
            case GameState.Move:
                break;
            case GameState.Inspect:
                break;
            case GameState.Dialog:
                break;
        }*/
       
       // OnGameStateChanged(newState);
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
