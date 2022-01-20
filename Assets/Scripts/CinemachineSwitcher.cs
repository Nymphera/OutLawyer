using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{   [SerializeField]
    private Animator Animator;
    private bool MainCameraState = true;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            SwitchState();
        }
    }
   
    
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
