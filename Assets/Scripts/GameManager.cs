using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isInputEnabled=true;
    [SerializeField]
    private GameState CurrentState;
   
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
        OnGameStateChanged(newState);
    }
   


}
    public enum GameState
    {
       Move,
       Interact,
        Dialog,
        Negotiations,
        Office,
        Location
    }
