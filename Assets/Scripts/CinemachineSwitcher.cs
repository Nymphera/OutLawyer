using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinemachineSwitcher : MonoBehaviour
{   [SerializeField]
    private Animator Animator;
    private bool MainCameraState = false;
    public static CinemachineSwitcher Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void SwitchState()
    {
        if (MainCameraState)
        {
            Animator.Play("PlayerCamera");
            GameManager.Instance.OpenPinBoard(true);
        }
        else
        {
            Animator.Play("InspectCamera");
            GameManager.Instance.OpenPinBoard(false);
        }
        MainCameraState = !MainCameraState;
    }
}
