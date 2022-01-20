using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinemachineSwitcher : MonoBehaviour
{   [SerializeField]
    private Animator Animator;
    private bool MainCameraState = false;

    
    public void SwitchState()
    {
        if (MainCameraState)
        {
            Animator.Play("PlayerCamera");
            
        }
        else
        {
            Animator.Play("InspectCamera");
        }
        MainCameraState = !MainCameraState;
    }
}
