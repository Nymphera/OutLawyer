using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PinBoardScript : MonoBehaviour
{   
    private Evidence PointedEvidence;
    private Camera cam;
   
    private Vector3 LocationPosition;
    private GameObject TeleportButton;
    public static PinBoardScript Instance;
    private GameObject Player;
    
   [SerializeField]
    private CinemachineVirtualCamera PinCamera; 
    

    private void Awake()
    {
        OfficeManager.OnStateChanged += OfficeManagerOnStateChanged;
        Instance = this;
        cam = Camera.main;

    }
   

    private void OnDestroy()
    {
        OfficeManager.OnStateChanged -= OfficeManagerOnStateChanged;

    }
    private void OfficeManagerOnStateChanged(OfficeState State)
    {
        PinCamera.GetComponent<PinBoardCamera>().enabled = (State == OfficeState.PinBoard);
        transform.GetChild(1).gameObject.SetActive(State == OfficeState.PinBoard);
    }
    private void Update()
    {
        
        if ( Input.GetMouseButton(1))
        {
            ShowOptions();
            Debug.Log("yes sir");
        
            
        }

    }

    public void SetPlayerLocation()
    {   
        Player = GameObject.Find("Player");
        LocationPosition = new Vector3(0, 0, -15);
        CinemachineSwitcher.Instance.SwitchState();
        Player.transform.position = LocationPosition;
        
        OfficeManager.Instance.UpdateOfficeState(OfficeState.MovingtoLocation);
    }
    public void ShowOptions()
    {
        if (IsTouchingEvidence() && PointedEvidence.evidenceType.ToString() == "Location")
        {
            
            Vector3 mousePosition= Input.mousePosition;
          
            TeleportButton.transform.position = mousePosition;
            Debug.Log("Teleport button: "+ TeleportButton.transform.localPosition);
            TeleportButton.SetActive(true);
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
