using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CinemachineSwitcher : MonoBehaviour
{   [SerializeField]
    private Animator Animator;
    private bool MainCameraState = false;
    public static CinemachineSwitcher Instance;
    private OfficeManager officeManager;
    
   

    private void Awake()
    {
        
        
       
        Instance = this;
        
    }

   

    public void SwitchState()
    {
        if (MainCameraState)
        {
            Animator.Play("Biuro Cam");


            OfficeManager.Instance.UpdateOfficeState(OfficeState.Overview);
        }
        else
        {
            Animator.Play("PinBoard Cam");
            OfficeManager.Instance.UpdateOfficeState(OfficeState.PinBoard);
        }
        MainCameraState = !MainCameraState;
    }
}
