using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Inspector : MonoBehaviour
{//[SerializeField]
    //private LayerMask Interact;
    [SerializeField]
    private Camera MainCamera;

    private CinemachineVirtualCamera Cinemachine;
    private Transform InspectObject;
    [SerializeField]
    private float CameraDistance;
    private void Start()
    {
        
        Cinemachine= GetComponent<CinemachineVirtualCamera>();
        InspectObject=Cinemachine.Follow;
    }

   
}
