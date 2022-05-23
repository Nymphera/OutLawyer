using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class OutlineManager : MonoBehaviour
{
    [SerializeField]
    private GameState currentState;
    
   
    
    
    
 
    
    private GameObject outlineObject;
    private GameControls gameControls;
    private InputAction mouseMove;

    private void Awake()
    {
        
        gameControls = new GameControls();
       
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
      
        gameControls.Game.MousePosition.performed += MousePosition_performed;
        mouseMove = gameControls.Game.MousePosition;
       // if(specialLogicOnClick!=null)
       // specialLogicOnClick.SetActive(false);
        //  actionTextField = GameObject.Find("InteractText").GetComponent<TextMeshProUGUI>();
    }  
    
    private void GameManager_OnGameStateChanged(GameState state)
    {
        currentState = state;
        if (state == GameState.Interact)
        {
            if(outlineObject!=null)
            DisableOutline(outlineObject);
        }
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
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        gameControls.Game.MousePosition.performed -= MousePosition_performed;
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
        
        
        if (Physics.Raycast(Ray, out hit,10f))
        {
            if (currentState == GameState.Move || currentState == GameState.Office)
            {
                if (hit.transform.GetComponent<Outline>() != null)
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);

                    outlineObject = selectedObj;
                }
            }
            if (currentState == GameState.Interact)
            {
                if (hit.transform.GetComponent<Outline>() != null)
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);                 

                    outlineObject = selectedObj;
                }
            }


        }
    }


    public void EnableOutline(GameObject Object)
    {
        Color color = Object.GetComponent<Outline>().OutlineColor;
        color.a = 1;
        Object.GetComponent<Outline>().OutlineColor=color;
        Object.GetComponent<Outline>().enabled = true;


    }

    public void DisableOutline(GameObject Object)
    {
        // HideActionDescription();
        if (Object.GetComponent<Outline>()!=null) 
        {
        Color color = Object.GetComponent<Outline>().OutlineColor;
        color.a = 0;
        Object.GetComponent<Outline>().OutlineColor = color;
        Object.GetComponent<Outline>().enabled = false;
        }
    }
  
        
    
}
