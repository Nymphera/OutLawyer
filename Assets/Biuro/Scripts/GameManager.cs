using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isInteractEnabled;
    public bool isMoveEnabled;
    public bool isPauseEnabled;
    public bool isLookEnabled;
    [SerializeField]
    public GameState CurrentState;
    
    public static event Action<GameState> OnGameStateChanged;

    public int keyCount=0;
    [SerializeField]
    public List<LineData> createdLines= new List<LineData>();
    [SerializeField]
    private List<Evidence> unlockedEvidences= new List<Evidence>();

    [SerializeField]
    private GameObject notatnik,mouse;
    private void Awake()
    {
        
        PinBoardManager.OnLineDeleted += OnLineDeleted;
        

        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

       
    }
    private void Start()
    {
        GameEvents.current.onBurnLines += OnnBurnLines;
        GameEvents.current.onLineCreated += OnLineCreated;
        GameEvents.current.onEvidneceUnlocked += UnlockEvidence;
        UpdateGameState(GameState.Office);
    }

   

    private void OnDestroy()
    {
        GameEvents.current.onEvidneceUnlocked -= UnlockEvidence;
        GameEvents.current.onLineCreated -= OnLineCreated;
        PinBoardManager.OnLineDeleted -= OnLineDeleted;
        GameEvents.current.onBurnLines -= OnnBurnLines;
    }
    private void UnlockEvidence(Evidence evidence)
    {
        
        unlockedEvidences.Add(evidence);
    }
    public Evidence[] GetUnlockedEvidences()
    {
        return unlockedEvidences.ToArray();
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
        OnGameStateChanged?.Invoke(newState);
        switch (newState)
        {
            case GameState.Office:
                {
                    Debug.Log("Office");
                    isInteractEnabled = true;
                    isPauseEnabled = true;
                   // isMoveEnabled = false;
                   
                   // isLookEnabled = false;
                    
                    mouse.SetActive(false);
                    notatnik.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                }
                break;
            case GameState.Move:
                {
                    isLookEnabled = true;
                    isMoveEnabled = true;
                    isInteractEnabled = true;
                    isPauseEnabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    
                    notatnik.SetActive(true);
                    mouse.SetActive(true);
                }
                break;
            case GameState.Interact:
                {
                    isMoveEnabled = false;
                    isInteractEnabled = true;
                    isLookEnabled = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    mouse.SetActive(false);
                    notatnik.SetActive(false);
                    //notatnik.SetActive(false);
                    //mouse.SetActive(false);
                }
                break;
            case GameState.LockInteract:
                {
                    isMoveEnabled = false;
                    //isInteractEnabled = false;
                    isLookEnabled = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                   
                    notatnik.SetActive(false);
                    mouse.SetActive(false);
                }
                break;
            case GameState.Prolog:
                {
                    isMoveEnabled = false;
                    isInteractEnabled = false;
                    isPauseEnabled = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;          

        }
    }
}
[Serializable]
    public enum GameState
    {
       Move,        //move and interact enable
       Interact,    //move disable interact enable
       LockInteract,    
       Dialog,      
        Office,       
        Prolog
    }
