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

    public OfficeState CurrentState;
    public static event Action<OfficeState> OnOfficeStateChanged;


    private void Awake()
    {
        Animator = transform.GetComponent<Animator>();
              
             
        if (Instance == null)
        {
            Instance = this;
         
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged; 
              
        
        
        
    }

    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
        CurrentState = state;
    }

    public void SwitchState(string objname)
    { 
        if (objname=="Biuro")
        {
            Animator.Play("Biuro Cam");
            OnOfficeStateChanged(OfficeState.Overview);
            GameManager.Instance.UpdateGameState(GameState.Move);


        }
        else if(objname=="PinBoardSprite")
        {
            Animator.Play("PinBoard Cam");
            OnOfficeStateChanged(OfficeState.PinBoard);
            
        }
        else if (objname == "KOMINEK")
        {
            Animator.Play("Fire Cam");
        }
        else if (objname=="Evidence")
        {
            Animator.Play("InspectCam");
            OnOfficeStateChanged(OfficeState.Inspect);
            
        }
        else 
        {
            Animator.Play("Desk Cam");
            OnOfficeStateChanged(OfficeState.Desk);
            GameManager.Instance.UpdateGameState(GameState.Interact);
        }
                    
        MainCameraState = !MainCameraState;
    }
}
public enum OfficeState
{
    Overview, //1
    Desk,
    PinBoard,
    Inspect
   

}