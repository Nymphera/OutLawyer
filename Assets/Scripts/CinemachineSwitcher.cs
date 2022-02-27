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
    [SerializeField]
    private InputAction action;
    private void Awake()
    {
        Instance = this;
    }
    public void SwitchState()
    {
        if (MainCameraState)
        {
            Animator.Play("Biuro Cam");
            //GameManager.Instance.OpenPinBoard(true);
        }
        else
        {
            Animator.Play("PinBoard Cam");
           // GameManager.Instance.OpenPinBoard(false);
        }
        MainCameraState = !MainCameraState;
    }
}
