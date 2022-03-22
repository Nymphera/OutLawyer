using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interact : MonoBehaviour
{


    [SerializeField]
    GameObject[] interactable;
    private GameObject selectedObject;

    private void Awake()
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

        if(Physics.Raycast(Ray,out hit)&&hit.transform.tag=="Interact")
        {
             selectedObj = hit.transform.gameObject;
            EnableOutline(selectedObj);


            selectedObject = selectedObj;
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
        {
            obj.AddComponent<MeshCollider>();
            obj.AddComponent<Outline>();
            Outline outline = obj.GetComponent<Outline>();
            outline.OutlineColor = Color.red;
            outline.OutlineWidth = 5f;
            outline.enabled = false;
        }
    }
}
