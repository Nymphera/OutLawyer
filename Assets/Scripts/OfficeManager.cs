using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfficeManager : MonoBehaviour
{
    public static event Action <OfficeState> OnStateChanged;
    
    public static OfficeManager Instance;
    private HelpLines HelpLines;

    public OfficeState State;
    private void Awake()
    {
        Instance = this;

        OnStateChanged(OfficeState.Overview);
    }
 

    void Start()
    {
        UpdateOfficeState(OfficeState.Overview);
    }


    public void UpdateOfficeState(OfficeState newState)
    {
        OnStateChanged?.Invoke(newState);
    }
} 


public enum OfficeState{
    Overview, //1
    Newspaper,  
    PinBoard,
    Dialogs,
    MovingtoLocation
    
}
