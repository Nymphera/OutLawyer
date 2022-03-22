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

   

    public void SwitchState(string objname)
    { Debug.Log(objname);
        if (objname=="Biuro")
        {
            Animator.Play("Biuro Cam");
            OnOfficeStateChanged(OfficeState.Overview);
            
           
        }
        else if(objname=="PinBoardSprite")
        {
            Animator.Play("PinBoard Cam");
            OnOfficeStateChanged(OfficeState.PinBoard);
            
        }
        else
        {
            Animator.Play("Desk Cam");
            OnOfficeStateChanged(OfficeState.Desk);
        }
                    
        MainCameraState = !MainCameraState;
    }
}
public enum OfficeState
{
    Overview, //1
    Desk,
    PinBoard,
   

}