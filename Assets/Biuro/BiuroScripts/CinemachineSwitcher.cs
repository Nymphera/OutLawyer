using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using System.Threading.Tasks;

public class CinemachineSwitcher : MonoBehaviour
{   [SerializeField]
    private Animator Animator;
    private bool MainCameraState = false;
    

    public OfficeState CurrentState;
    public static event Action<OfficeState> OnOfficeStateChanged;


    private void Awake()
    {
        Animator = transform.GetComponent<Animator>();
      

       // OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;      
    }
    private void Start()
    {
        GameEvents.current.onOfficeClick += SwitchState;
    }
    private void OnDestroy()
    {
        GameEvents.current.onOfficeClick -= SwitchState;
        OnOfficeStateChanged -= CinemachineSwitcher_OnOfficeStateChanged;
    }
    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
        CurrentState = state;
    }
    
    public async void SwitchState(int ID)
    {
        if (ID == 0)
        {
            Animator.Play("Biuro Cam");
            OnOfficeStateChanged(OfficeState.Overview);
            await Task.Delay(10);
            GameManager.Instance.UpdateGameState(GameState.Office);
        }
        else if (ID == 1)
        {
            Animator.Play("PinBoard Cam");
            OnOfficeStateChanged(OfficeState.PinBoard);
            GameManager.Instance.UpdateGameState(GameState.LockInteract);
            PinBoardManager.Instance.currentState = PinBoardState.Neutral;
        }
        else if (ID == 2)
        {
            Animator.Play("Desk Cam");
            OnOfficeStateChanged(OfficeState.Desk);
            GameManager.Instance.UpdateGameState(GameState.Interact);
        }
        else if (ID == 3)
        {
            Animator.Play("Fire Cam");
            GameManager.Instance.UpdateGameState(GameState.LockInteract);
        }
        else if (ID == 4)
        {
            Animator.Play("InspectCam");
            OnOfficeStateChanged(OfficeState.Inspect);
            GameManager.Instance.UpdateGameState(GameState.LockInteract);
            PinBoardManager.Instance.currentState = PinBoardState.Inspect;

        }
        else
            Debug.Log("Do nothing");
        
                    
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