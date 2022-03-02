using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask Ground;
    [SerializeField]
    private float PlayerSpeed=6,turnSmoothTime=0.1f;
    float turnSmoothVelocity;
    private CharacterController CharacterController;
    


    private PlayerMovementActions PlayerInputActions;
    private InputAction movement;
    
    private NavMeshAgent Player;
    Ray myRay;
    private void Awake()
    { PlayerInputActions = new PlayerMovementActions();
        CharacterController = GetComponent<CharacterController>();
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
        Vector2 direction = movement.ReadValue<Vector2>();
        float targetAngle=Mathf.Atan2(direction.x,direction.y)*Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        
        CharacterController.Move(InputToVector3(direction)*Time.deltaTime*PlayerSpeed);
    }

    private Vector3 InputToVector3(Vector2 direction)
    {
        Vector3 Direction = Vector3.zero;
        Direction.x = direction.x;
        
        Direction.z = direction.y;
        return Direction;
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
