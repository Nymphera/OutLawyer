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
        CurrentState = newState;
        OnGameStateChanged(newState);
        if (newState == GameState.Location)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
   

   

}
    public enum GameState
    {
       Move,        //move and interact enable
       Interact,    //move disable interact enable
       LockInteract,    
        Dialog,     //move diable interact disable
        Negotiations,   
        Office,
        Location
    }
