using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private OfficeState currentState;
    [SerializeField]
    List<GameObject> interactable, interactable2;
    [SerializeField]
    private Color OutlineColor,TransparentColor;

    private GameObject outlineObject;
    private GameControls gameControls;
    private InputAction mouseMove;

    private void Awake()
    {

        gameControls = new GameControls();
        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
        
        interactable = new List<GameObject>();
        interactable.AddRange(GameObject.FindGameObjectsWithTag("Interact"));

        interactable2 = new List<GameObject>();
        interactable2.AddRange(GameObject.FindGameObjectsWithTag("Interact2"));

        TransparentColor = OutlineColor;
        TransparentColor.a = 0;

        StartCoroutine(CreateOutline(interactable));
        StartCoroutine(CreateOutline(interactable2));
        gameControls.Game.MousePosition.performed += MousePosition_performed;
        mouseMove = gameControls.Game.MousePosition;
    }
    private void OnEnable()
    {
        gameControls.Enable();
    }
    private void OnDisable()
    {
        gameControls.Disable();
    }

    private void MousePosition_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (outlineObject != null)
        {
            DisableOutline(outlineObject);
            outlineObject = null;
        }

        GameObject selectedObj;
        Ray Ray = Camera.main.ScreenPointToRay(mouseMove.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit))
        {
            if (currentState==OfficeState.Overview)
            {
                if (hit.transform.tag == "Interact")
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);


                    outlineObject = selectedObj;
                }
            }
            if (currentState == OfficeState.Desk)
            {
                if (hit.transform.tag == "Interact2")
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);


                    outlineObject = selectedObj;
                }
            }

        }
    }

    private void OnDestroy()
    {
        CinemachineSwitcher.OnOfficeStateChanged -= CinemachineSwitcher_OnOfficeStateChanged;
        gameControls.Game.MousePosition.performed -= MousePosition_performed;
    }

    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
        currentState = state;
        
        
        
        if (state == OfficeState.Desk)
        {
            DisableAllOutlines(interactable);
            
        }
        else if (state == OfficeState.Overview)
        {
            DisableAllOutlines(interactable2);
        }
        else
        {
            DisableAllOutlines(interactable2);
            DisableAllOutlines(interactable);
        }
    }

    private void EnableOutline(GameObject Object)
    {
       Object.GetComponent<Outline>().enabled=true;  
    } 
    private void DisableAllOutlines(List<GameObject> interactable)
    {
        foreach(GameObject obj in interactable)
        {
            obj.GetComponent<Outline>().enabled = false;
        }
    }
    private void DisableOutline(GameObject Object)
    {
        Object.GetComponent<Outline>().enabled=false;
    }
    private IEnumerator CreateOutline(List<GameObject> interact)
    {
        foreach (GameObject obj in interact)
        {
            
            if(obj.GetComponent<MeshCollider>()==null)
            obj.AddComponent<MeshCollider>();
            yield return null;
            if (obj.GetComponent<Outline>() == null)
            {
                obj.AddComponent<Outline>();
                yield return null;
                obj.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
                obj.GetComponent<Outline>().OutlineColor = OutlineColor;    
                obj.GetComponent<Outline>().OutlineWidth = 5f;
                obj.GetComponent<Outline>().enabled = false;
                

            }
            yield return null; 
        }
    }
}
