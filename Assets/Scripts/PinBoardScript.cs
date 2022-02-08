using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinBoardScript : MonoBehaviour
{
    private Evidence Evidence;
    private Evidence PointedEvidence;
    private Camera cam;
    private GameObject Player;
    private Vector3 LocationPosition;
    private GameObject Teleport;
    private void Start()
    {
        Teleport = GameObject.Find("Teleport");
        Teleport.SetActive(false);
        cam = Camera.main;
        Player = GameObject.Find("Player");

    }
  

    // Update is called once per frame
    private void Update()
    {
        
        if ( Input.GetMouseButton(1))
        {
            ShowOptions();
            Debug.Log("yes sir");
        
            
        }

    }

    public void SetPlayerLocation()
    {   //lokacja musi mieæ tak¹ sam¹ nazwê jak jej dowód
        
        LocationPosition = GameObject.Find(PointedEvidence.Name).transform.position;
        CinemachineSwitcher.Instance.SwitchState();
        Player.transform.position = LocationPosition;

    
    }
    public void ShowOptions()
    {
        if (IsTouchingEvidence() && PointedEvidence.evidenceType.ToString() == "Location")
        {
            
            Vector3 mousePosition= Input.mousePosition;
            Debug.Log("mouse position " + mousePosition);
            Teleport.transform.position = mousePosition;
            Debug.Log("Teleport button: "+ Teleport.transform.localPosition);
            Teleport.SetActive(true);
        }
        else Debug.Log("no options!");
    }
    public bool IsTouchingEvidence()
    {

        Ray Ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        GameObject PointedObject;

        if (Physics.Raycast(Ray, out Hit, 1000))
        {
            if (Hit.transform.gameObject.layer == 7)
            {
                Debug.Log("Yes");
                PointedObject = Hit.transform.gameObject;
                Debug.Log(PointedObject);
                PointedEvidence = PointedObject.GetComponent<EvidenceDisplay>().Evidence;

                return true;

            }
            else
            {
     
                return false;
            }
               
        }
        else
        {
          
            return false;
        }
           
    }
    
}
