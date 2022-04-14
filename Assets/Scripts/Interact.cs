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
    private GameState currentState;
    
   
    public string actionDescription;
    
    
    private TextMeshProUGUI actionTextField;
    public  GameObject specialLogicOnClick;
    private GameObject outlineObject;
    private GameControls gameControls;
    private InputAction mouseMove;

    private void Awake()
    {
        
        gameControls = new GameControls();
       
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
      
        gameControls.Game.MousePosition.performed += MousePosition_performed;
        mouseMove = gameControls.Game.MousePosition;

      //  actionTextField = GameObject.Find("InteractText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        AddRenderer();

        HideActionDescription();
        SetOutline();
    }
    private void GameManager_OnGameStateChanged(GameState state)
    {
        currentState = state;
        if (state == GameState.Interact)
        {
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
    private void OnMouseDown()
    {
        specialLogicOnClick.SetActive(true);
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
            if (currentState == GameState.Move)
            {
                if (hit.transform.tag == "Interact")
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);
                    //ShowActionDescription(selectedObj.GetComponent<Outline>());

                    outlineObject = selectedObj;
                }
            }
            if (currentState == GameState.Interact)
            {
                if (hit.transform.tag == "Interact2")
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);
                   // ShowActionDescription(selectedObj.GetComponent<Outline>());

                    outlineObject = selectedObj;
                }
            }


        }
    }
    private void AddRenderer()
    {
        if (gameObject.GetComponent<MeshCollider>() == null)
        {
            gameObject.AddComponent<MeshCollider>();
        }
        else
        {
            Debug.Log(transform.name + " has already collider");
        }
    }
    public void SetOutline()
    {
        if (gameObject.GetComponent<Outline>() == null)
        {
            Debug.Log("You have to add Outline Component to " + transform.name);
        }
        else
        {
            DisableOutline(gameObject);
        }
    }
 

    private void EnableOutline(GameObject Object)
    {
        Color color = Object.GetComponent<Outline>().OutlineColor;
        color.a = 1;
        Object.GetComponent<Outline>().OutlineColor=color;
        Object.GetComponent<Outline>().enabled = true;


    }

    private void ShowActionDescription(Outline outline)
    {
        string actionText;
        actionTextField.enabled = true;
        Vector2 mousePos=gameControls.Game.MousePosition.ReadValue<Vector2>();
        if (this.actionTextField != null)
            actionText = actionDescription;
        else
            actionText = "Inspect";
        
        if(mousePos.x> Screen.width / 2)
        {
            Vector2 textPosition = new Vector2(mousePos.x - 300,mousePos.y);
           actionTextField.rectTransform.position = textPosition;
            actionTextField.text = actionText+"   \u2022";
        }
        
        else if(mousePos.x <= Screen.width / 2)
        {
            Vector2 textPosition = new Vector2(mousePos.x+300, mousePos.y);
            actionTextField.rectTransform.position = textPosition;
            actionTextField.text = "\u2022   "+actionText;
        }
        //actionDescription.text = "\u2022<indent=3em>The text that will be indented.</indent>";
    }
    private void HideActionDescription()
    {
       // actionTextField.enabled = false;
    }

  
    private void DisableOutline(GameObject Object)
    {
       // HideActionDescription();
        Color color = Object.GetComponent<Outline>().OutlineColor;
        color.a = 0;
        Object.GetComponent<Outline>().OutlineColor = color;
        Object.GetComponent<Outline>().enabled = false;
    }
  
        
    
}
