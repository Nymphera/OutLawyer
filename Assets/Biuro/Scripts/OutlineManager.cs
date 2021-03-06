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

    //TEXT NA EKRANIE

    [SerializeField]
    private GameObject helpText;
    [SerializeField]
    private GameObject message;
    [SerializeField]
    private float displaySentencesTime=2.5f;

    //INVENTORY
    
    
    private void Awake()
    {
        
        gameControls = new GameControls();
       
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
      
        gameControls.Game.MousePosition.performed += MousePosition_performed;
        gameControls.Game.MouseLeftClick.performed += MouseLeftClick_performed;
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
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        gameControls.Game.MousePosition.performed -= MousePosition_performed;
        gameControls.Game.MouseLeftClick.performed -= MouseLeftClick_performed;
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        currentState = state;
        if (state == GameState.Interact || state == GameState.LockInteract)
        {
            if (outlineObject != null)
                DisableOutline(outlineObject);
        }
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
           if (GameManager.Instance.isInteractEnabled)
            {
                if (hit.transform.GetComponent<Outline>() != null)
                {
                    selectedObj = hit.transform.gameObject;
                    EnableOutline(selectedObj);
                    
                    outlineObject = selectedObj;
                    if (currentState == GameState.Move)
                    {
                        ShowInteractText();
                    }
                    
                }
            }
        }
    }

    private void MouseLeftClick_performed(InputAction.CallbackContext obj)
    {
        if (message != null && helpText != null)
        {
            if (outlineObject != null)
            {
                Outline outline = outlineObject.GetComponent<Outline>();
                string[] message = outline.message;
                Queue<string> sentences = new Queue<string>();
                foreach (string sentence in message)
                {
                    sentences.Enqueue(sentence);
                }
                StartCoroutine(DisplaySentences(sentences));

                if (outline.unlockEvidence)
                {
                    outline.unlockEvidence = false;
                    Evidence evidence = outline.evidenceToUnlock;
                    UnlockEvidence(evidence);
                }
            }          
        }
    }

    private void UnlockEvidence(Evidence evidence)
    {
        GameEvents.current.TriggerEvidenceUnlocked(evidence);
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
        if (helpText != null)
        {
            TextMeshProUGUI tmp = helpText.GetComponent<TextMeshProUGUI>();
            tmp.text = "";
        }

    }

    private void ShowInteractText()
    {
        
        if (helpText != null && currentState == GameState.Move) ;
        {
            TextMeshProUGUI tmp = helpText.GetComponent<TextMeshProUGUI>();
            if (outlineObject.GetComponent<InventoryItem>() !=null)
            {
                tmp.text = "[E] Pick Up";
            }
            else
            {
                tmp.text = "[LPM] Interact";
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
