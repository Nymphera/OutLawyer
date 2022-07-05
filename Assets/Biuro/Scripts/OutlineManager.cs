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
    [SerializeField]
    private GameObject helpText;
    [SerializeField]
    private GameObject message;
    
    private GameObject outlineObject;
    private GameControls gameControls;
    private InputAction mouseMove;
    [SerializeField]
    private float displaySentencesTime=3f;
    private void Awake()
    {
        
        gameControls = new GameControls();
       
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
      
        gameControls.Game.MousePosition.performed += MousePosition_performed;
        gameControls.Game.MouseLeftClick.performed += MouseLeftClick_performed;
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
        gameControls.Game.MouseLeftClick.performed -= MouseLeftClick_performed;
    }
    
    private void MousePosition_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        if (outlineObject != null)
        {
            DisableOutline(outlineObject);
            HideInteractText();
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
                    ShowInteractText();
                    outlineObject = selectedObj;
                }
            }
            if (currentState == GameState.Interact)
            {
                if (hit.transform.GetComponent<Outline>() != null)
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);
                    ShowInteractText();
                    outlineObject = selectedObj;
                    
                   
                }
            }


        }
    }
    private void MouseLeftClick_performed(InputAction.CallbackContext obj)
    {
        if (outlineObject != null)
        {
            string[] message = outlineObject.GetComponent<Outline>().message;
            Queue<string> sentences = new Queue<string>();
            foreach (string sentence in message)
            {
                sentences.Enqueue(sentence);
            }
            StartCoroutine(DisplaySentences(sentences));
        }
    }

    private IEnumerator DisplaySentences(Queue<string> sentences)
    {
        TextMeshProUGUI tmp = message.GetComponent<TextMeshProUGUI>();
        while (sentences.Count != 0)
        {
            string sentence = sentences.Dequeue();
            tmp.text = sentence;
            yield return new WaitForSeconds(displaySentencesTime);
        }
        tmp.text = "";
    }
    private void HideInteractText()
    {
        TextMeshProUGUI tmp = helpText.GetComponent<TextMeshProUGUI>();
        tmp.text = "";
    }

    private void ShowInteractText()
    {
        TextMeshProUGUI tmp = helpText.GetComponent<TextMeshProUGUI>();
        tmp.text = "[LPM] Interact";
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
