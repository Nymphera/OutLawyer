using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    private GameObject PlayerBody;
    [SerializeField]
    private LayerMask Ground;
    [SerializeField]
    private float PlayerSpeed = 6f;
        [SerializeField]
        private float turnSmoothTime=5f;
    float turnSmoothVelocity;
    private CharacterController CharacterController;
    private Camera cam;


    private PlayerMovementActions PlayerInputActions;
    private InputAction movement;
    
    private NavMeshAgent Player;
    Ray myRay;
    private void Awake()
    { PlayerInputActions = new PlayerMovementActions();
        CharacterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }
    void Start()
    {   
        Player = GetComponent<NavMeshAgent>();
        
    }
    private void OnEnable()
    {
        movement = PlayerInputActions.Player.Move; 
        movement.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 Input = movement.ReadValue<Vector2>();
        Vector3 Direction = new Vector3(Input.x, 0f, Input.y);

        Debug.Log(Direction);
        
        float targetAngle=Mathf.Atan2(Direction.x,Direction.z)*Mathf.Rad2Deg+cam.transform.eulerAngles.y;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //PlayerBody.GetComponent<Rigidbody>().AddForce(Direction *PlayerSpeed); 
        Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f)*Vector3.forward;
        if(Direction.magnitude>0)
        CharacterController.Move(moveDirection  * PlayerSpeed * Time.deltaTime);
    }

 

    private void OnDisable()
    {
        movement.Disable();
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
