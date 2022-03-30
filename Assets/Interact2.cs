using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact2 : MonoBehaviour
{[SerializeField]
    List<GameObject> interactable2;
    private GameObject selectedObject;
    private void Awake()
    {
        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
    }
    private void OnDestroy()
    {
        CinemachineSwitcher.OnOfficeStateChanged -= CinemachineSwitcher_OnOfficeStateChanged;
    }

    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {

        if (OfficeState.Desk == state)
        {
            interactable2 = new List<GameObject>();
            interactable2.AddRange(GameObject.FindGameObjectsWithTag("Interact2"));

           
            CreateOutline(interactable2);
            
        }
    }
    private void CreateOutline(List<GameObject> interact)
    {
        foreach (GameObject obj in interact)
        {
            if (obj.gameObject.GetComponent<MeshCollider>() == null)
                obj.gameObject.AddComponent<MeshCollider>();
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

        if (Physics.Raycast(Ray, out hit))
        {
            if (hit.transform.tag == "Interact2")
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
}
