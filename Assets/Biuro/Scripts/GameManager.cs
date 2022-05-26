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

    public int keyCount=0;
    [SerializeField]
    public List<LineData> createdLines= new List<LineData>();
    private void Awake()
    {
        PinBoardManager.OnLineCreated += OnLineCreated;
        PinBoardManager.OnLineDeleted += OnLineDeleted;
        

        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //OnGameStateChanged?.Invoke(GameState.Office);
    }
    private void Start()
    {
        GameEvents.current.onBurnLines += OnnBurnLines;
    }
    private void OnDestroy()
    {
        PinBoardManager.OnLineCreated -= OnLineCreated;
        PinBoardManager.OnLineDeleted -= OnLineDeleted;
        GameEvents.current.onBurnLines -= OnnBurnLines;
    }
    private void OnnBurnLines(Line line)
    {
        LineData lineData = new LineData();
        lineData.firstEvidence = line.firstEvidence;
        lineData.secondEvidence = line.secondEvidence;
        lineData.isConectionGood = line.isConectionGood;
        lineData.wasLineBurned = line.wasLineBurned;
        lineData.conectionType = line.conectionType;

        for (int i = 0; i < createdLines.Count; i++)
        {
            if (createdLines[i].firstEvidence == lineData.firstEvidence)
                if(createdLines[i].secondEvidence== lineData.secondEvidence)
                    if(lineData.conectionType == createdLines[i].conectionType)
            {
                createdLines[i].wasLineBurned = true;
            }
        }
        
    }

    private void OnLineDeleted(Line line)
    {
        foreach(LineData lineData in createdLines)
        {
            if(lineData.firstEvidence == line.firstEvidence)
                if(lineData.secondEvidence == line.secondEvidence)
                   if (lineData.conectionType == line.conectionType)
                    {
                        createdLines.Remove(lineData);
                        break;
                    }
                        
        }
    }

    private void OnLineCreated(Line line)
    {
        LineData lineData=new LineData();
        lineData.firstEvidence = line.firstEvidence;
        lineData.secondEvidence = line.secondEvidence;
        lineData.isConectionGood = line.isConectionGood;
        lineData.wasLineBurned = line.wasLineBurned;
        lineData.conectionType = line.conectionType;

        createdLines.Add(lineData);
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
