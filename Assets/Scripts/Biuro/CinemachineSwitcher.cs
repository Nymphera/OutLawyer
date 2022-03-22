using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class CinemachineSwitcher : MonoBehaviour
{   [SerializeField]
    private Animator Animator;
    private bool MainCameraState = false;
    public static CinemachineSwitcher Instance;

    public OfficeState State;
    public static event Action<OfficeState> OnOfficeStateChanged;


    private void Awake()
    {
        Instance = this;
        
    }

   

    public void SwitchState()
    {
        if (MainCameraState)
        {
            Animator.Play("Biuro Cam");
            OnOfficeStateChanged(OfficeState.Overview);
            
           
        }
        else
        {
            Animator.Play("PinBoard Cam");
            OnOfficeStateChanged(OfficeState.PinBoard);
            
        }
        MainCameraState = !MainCameraState;
    }
}
public enum OfficeState
{
    Overview, //1
    Newspaper,
    PinBoard,
    Dialogs,
    MovingtoLocation

}