using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class InspectCameraContoller : MonoBehaviour
{
    CinemachineVirtualCamera camera;
    private GameControls GameControls;
    GameObject panel;
    Vector2 mousePosition;
    TextMeshProUGUI tmp;
    private void Awake()
    {
        GameControls = new GameControls();
        camera = GetComponent<CinemachineVirtualCamera>();
        panel= GameObject.Find("SettingsPanel");
        GameControls.Game.MousePosition.performed += OnMouseMove;
        GameControls.Game.MouseLeftClick.performed += OnMouseClick;
        tmp = panel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        panel.SetActive(false);
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
            if (PinBoardManager.Instance.currentState == PinBoardState.Neutral&&GameManager.Instance.CurrentState==GameState.LockInteract)
            {
              StartCoroutine(  Inspect(Hit));
            }
        }
    }

    private void OnMouseMove(InputAction.CallbackContext obj)
    {
        mousePosition = GameControls.Game.MousePosition.ReadValue<Vector2>();
    }
    private IEnumerator Inspect(RaycastHit Hit)
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
            yield return new WaitForSeconds(1f);
            
            panel.SetActive(true);
            tmp.text = evidence.Description;
        }

        

    }
}
