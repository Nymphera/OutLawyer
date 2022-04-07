using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerCameraChanger : MonoBehaviour
    {
    private Animator Animator;
    private GameControls gameControls;
    private bool switchState=true;
    private void Awake()
    {
        Animator = transform.GetComponent<Animator>();
        gameControls = new GameControls();
        gameControls.Game.ChangePlayerCamera.performed += SwitchState;
    }
    private void OnEnable()
    {
        gameControls.Enable();
    }
    private void OnDisable()
    {
        gameControls.Disable();
    }
    private void OnDestroy()
    {
        gameControls.Game.ChangePlayerCamera.performed -= SwitchState;
    }
    private void SwitchState(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!switchState)
        {
            Animator.Play("FirstPerson");
        }
        else
        {
            Animator.Play("ThirdPerson");
        }
        switchState = !switchState;
    }
}

