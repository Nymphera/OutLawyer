using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System;

public class InspectCameraContoller : MonoBehaviour
{
    CinemachineVirtualCamera camera;
    private GameControls GameControls;
    Vector2 mousePosition;
    private void Awake()
    {
        GameControls = new GameControls();
        camera = GetComponent<CinemachineVirtualCamera>();

        GameControls.Game.MousePosition.performed += OnMouseMove;
        GameControls.Game.MouseLeftClick.performed += OnMouseClick;
    }
    private void OnEnable()
    {
        GameControls.Enable();
    }
    private void OnDisable()
    {
        GameControls.Disable();
    }
    private void OnDestroy()
    {
        GameControls.Game.MousePosition.performed -= OnMouseMove;
        GameControls.Game.MouseLeftClick.performed -= OnMouseClick;
    }
    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        Ray Ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit Hit;

       

        if (Physics.Raycast(Ray, out Hit, 1000))
        {
            if (PinBoardManager.Instance.currentState == PinBoardState.Neutral)
            {
                Inspect(Hit);
            }
        }
    }

    private void OnMouseMove(InputAction.CallbackContext obj)
    {
        mousePosition = GameControls.Game.MousePosition.ReadValue<Vector2>();
    }
    private void Inspect(RaycastHit Hit)
    {
        GameObject currentEvidence;
        Evidence evidence;
        if (Hit.transform.gameObject.layer == 7)
        {
           
            currentEvidence = Hit.transform.parent.gameObject;
            if (currentEvidence.name == "Pin")
            {
                currentEvidence = currentEvidence.transform.parent.gameObject;
            }
               
            evidence = currentEvidence.GetComponent<EvidenceDisplay>().Evidence;
            camera.Follow = currentEvidence.transform;
            camera.LookAt = currentEvidence.transform;

            GameEvents.current.OfficeClick(4);
        }

        

    }
}
