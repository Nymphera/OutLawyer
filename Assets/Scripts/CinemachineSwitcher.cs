using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinemachineSwitcher : MonoBehaviour
{   [SerializeField]
    private Animator Animator;
    private bool MainCameraState = true;
    private Button PinboardButton;
    void Start()
    {
        PinboardButton = PinboardButton.GetComponent<Button>();
        PinboardButton = GameObject.Find("PinBoard Button").GetComponent<Button>();
    }
   
    private void Update()
    {
        if (PinboardButton.IsActive())
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
