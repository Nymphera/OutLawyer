using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isInputEnabled,isMoveEnabled,isPauseEnabled;
    
    public GameState CurrentState;
   
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
    private void Update()
    {

    }


    public void UpdateGameState(GameState newState)
    {
        CurrentState = newState;
        OnGameStateChanged(newState);
        switch (newState)
        {
            case GameState.Office:
                {
                    Cursor.lockState = CursorLockMode.None;
                    isMoveEnabled = false;
                    isInputEnabled = true;
                }
                break;
            case GameState.Move:
                {
                    isMoveEnabled = true;
                    isInputEnabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
            case GameState.Interact:
                {
                    isMoveEnabled = false;
                    isInputEnabled = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            case GameState.LockInteract:
                {
                    isMoveEnabled = false;
                    isInputEnabled = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    
                }
                break;
            case GameState.CutScene:
                {
                    isMoveEnabled = false;
                    isInputEnabled = false;
                    isPauseEnabled = false;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            

        }




    }
   

   

}
    public enum GameState
    {
       Move,        //move and interact enable
       Interact,    //move disable interact enable
       LockInteract,    
       Dialog,
      
        Office,
       
        CutScene
    }
