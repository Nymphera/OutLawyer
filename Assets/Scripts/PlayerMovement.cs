using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask Ground;
    [SerializeField]
    private Camera Inspector;
    private InputActionMap PlayerInput;
    
    
    private NavMeshAgent Player;
    Ray myRay;
    void Start()
    {   
        Player = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    { 
       
    }
    public void Move()
    {
        Debug.Log("move!");
        Vector3 mouse = Input.mousePosition;
        myRay = Camera.main.ScreenPointToRay(mouse);
        RaycastHit HitInfo;
        if (Physics.Raycast(myRay, out HitInfo, 100, Ground))
        {
            Player.SetDestination(HitInfo.point);
        }
       
    }
    private void Inspect()
    {
        
        //CinemachineSwitcher.SwitchState();
    }
}
