using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interact : MonoBehaviour
{


    [SerializeField]
    GameObject[] interactable;
    private GameObject selectedObject;
    public Interact Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        interactable = GameObject.FindGameObjectsWithTag("Interact");
        
        CreateOutline();
        
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
    private void DisableOutline(GameObject Object)
    {
        Outline outline = Object.GetComponent<Outline>();
        outline.enabled = false;
    }
    private void CreateOutline()
    {
        foreach (GameObject obj in interactable)
        {if(obj.GetComponent<MeshCollider>()==null)
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
