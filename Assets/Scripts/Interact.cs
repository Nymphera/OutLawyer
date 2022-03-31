using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{

    private OfficeState currentState;
    [SerializeField]
    List<GameObject> interactable;
  
    
    private GameObject selectedObject;
    
    private void Awake()
    {

        
        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
        
        interactable = new List<GameObject>();
        interactable.AddRange(GameObject.FindGameObjectsWithTag("Interact"));
        
        
        
        CreateOutline(interactable);


    }
    private void Start()
    {
        foreach (GameObject obj in interactable)
        {
            if (obj != null)
            {
                Outline outline = obj.GetComponent<Outline>();
                outline.enabled = false;
            }
            else
                interactable.Remove(obj);
        }
    }

   

    private void OnDestroy()
    {
       
        CinemachineSwitcher.OnOfficeStateChanged -= CinemachineSwitcher_OnOfficeStateChanged;

    }

    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
        currentState = state;
        DisableAllOutlines(interactable);
            this.enabled = (OfficeState.Overview == state);
        
    }



    private void Update()
    {
        
        
        if (selectedObject != null)
        {
            DisableOutline(selectedObject);
            selectedObject = null;
        }

        GameObject selectedObj;
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(Ray,out hit))
        {if(hit.transform.tag == "Interact")
            {
                selectedObj = hit.transform.gameObject;
                EnableOutline(selectedObj);


                selectedObject = selectedObj;
            }        
             
        }
        
       
    }
     
    private void EnableOutline(GameObject Object)
    {
        Outline outline = Object.GetComponent<Outline>();
        outline.enabled = true;
    } 
    private void DisableAllOutlines(List<GameObject> interactable)
    {
        foreach(GameObject obj in interactable)
        {
            if (obj != null)
            {
                Outline outline = obj.GetComponent<Outline>();
                outline.enabled = false;
            }
            else
                interactable.Remove(obj);
           
        }
    }
    private void DisableOutline(GameObject Object)
    {
        Outline outline = Object.GetComponent<Outline>();
        outline.enabled = false;
    }
    private void CreateOutline(List<GameObject> interact)
    {
        foreach (GameObject obj in interact)
        {
            
            if(obj.GetComponent<MeshCollider>()==null)
            obj.AddComponent<MeshCollider>();
            if (obj.GetComponent<Outline>() == null)
            {
                obj.AddComponent<Outline>();
                Outline outline = obj.GetComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineVisible;
                outline.OutlineColor = Color.red;     //trochê nie wiem dlaczego, ale nie zapisuje siê outline.color, mo¿e dlatego ¿e za ka¿dym razem dodaje nowy outline do gry
                outline.OutlineWidth = 5f;
                outline.enabled = false;
                
            }
                
        }
    }
}
