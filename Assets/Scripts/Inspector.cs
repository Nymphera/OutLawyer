using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Inspector : MonoBehaviour
{[SerializeField]
    private LayerMask Interact;
    [SerializeField]
    private Camera MainCamera;

    private CinemachineVirtualCamera Cinemachine;
    private Transform InspectObject;
    private void Start()
    {
        
        Cinemachine= GetComponent<CinemachineVirtualCamera>();
        InspectObject=Cinemachine.Follow;
    }
    void Update()
    {
        
        
            InspectObject = MousePoininting();
            print(InspectObject.name);

    }
      Transform MousePoininting()
       {
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit HitInfo;
        if (Physics.Raycast(Ray, out HitInfo, 100, Interact))
        {
            return HitInfo.transform;
        }
        else
            return null;
           

       }
}
