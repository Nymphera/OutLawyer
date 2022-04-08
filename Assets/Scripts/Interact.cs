using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private OfficeState currentState;
    [SerializeField]
    List<GameObject> interactable, interactable2;
    [SerializeField]
    private Color OutlineColor;
    [SerializeField]
    private float outlineWidth=3f;
    [SerializeField]
    private TextMeshProUGUI actionDescription;
    private GameObject outlineObject;
    private GameControls gameControls;
    private InputAction mouseMove;

    private void Awake()
    {

        HideActionDescription();

        gameControls = new GameControls();
        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
        
        interactable = new List<GameObject>();
        interactable.AddRange(GameObject.FindGameObjectsWithTag("Interact"));

        interactable2 = new List<GameObject>();
        interactable2.AddRange(GameObject.FindGameObjectsWithTag("Interact2"));

        

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
            if (currentState == OfficeState.Overview)
            {
                if (hit.transform.tag == "Interact")
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);
                    //ShowActionDescription(selectedObj.GetComponent<Outline>());

                    outlineObject = selectedObj;
                }
            }
            if (currentState == OfficeState.Desk)
            {
                if (hit.transform.tag == "Interact2")
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);
                    ShowActionDescription(selectedObj.GetComponent<Outline>());

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
        Outline outline = Object.GetComponent<Outline>();
       Object.GetComponent<Outline>().enabled=true;
        
    }

    private void ShowActionDescription(Outline outline)
    {
        string actionText;
        actionDescription.enabled = true;
        Vector2 mousePos=gameControls.Game.MousePosition.ReadValue<Vector2>();
        if (outline.ActionDescription != null)
            actionText = outline.ActionDescription;
        else
            actionText = "Inspect";
        
        if(mousePos.x> Screen.width / 2)
        {
            Vector2 textPosition = new Vector2(mousePos.x - 300,mousePos.y);
            actionDescription.rectTransform.position = textPosition;
            actionDescription.text = actionText+"   \u2022";
        }
        
        else if(mousePos.x <= Screen.width / 2)
        {
            Vector2 textPosition = new Vector2(mousePos.x+300, mousePos.y);
            actionDescription.rectTransform.position = textPosition;
            actionDescription.text = "\u2022   "+actionText;
        }
        //actionDescription.text = "\u2022<indent=3em>The text that will be indented.</indent>";
    }
    private void HideActionDescription()
    {
        actionDescription.enabled = false;
    }

    private void DisableAllOutlines(List<GameObject> interactable)
    {
        
            foreach (GameObject obj in interactable)
        {
            obj.GetComponent<Outline>().enabled = false;
        }
    }
    private void DisableOutline(GameObject Object)
    {
        HideActionDescription();
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
                obj.AddComponent<Outline>();
            
                
                yield return null;
                obj.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
                obj.GetComponent<Outline>().OutlineColor = OutlineColor;    
                obj.GetComponent<Outline>().OutlineWidth = outlineWidth;
                obj.GetComponent<Outline>().enabled = false;
                

            
            yield return null;
            
        }
        
    }
}
