using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{   


    [SerializeField]
    private float PlayerSpeed = 6f;
        [SerializeField]
        private float turnSmoothTime=5f;
    private float turnSmoothVelocity;
    private CharacterController CharacterController;
    private Camera cam;
    public static event Action OnEvidenceUnlocked;

    private PlayerMovementActions PlayerInputActions;
    private InputAction movement;
    
    private NavMeshAgent Player;
    Ray myRay;
    private void Awake()
    { PlayerInputActions = new PlayerMovementActions();
        CharacterController = GetComponent<CharacterController>();
        Player = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState State)
    {
        Player.GetComponent<PlayerController>().enabled = (State == GameState.Move);

    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }
  
    private void OnEnable()
    {
        movement = PlayerInputActions.Player.Move; 
        movement.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 Input = movement.ReadValue<Vector2>();
        Move(Input);



    }

 

    private void OnDisable()
    {
        movement.Disable();
    }
    // Update is called once per frame
   
    public void Move(Vector2 Input)
    {
        Vector3 Direction = new Vector3(Input.x, 0f, Input.y);

        float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //PlayerBody.GetComponent<Rigidbody>().AddForce(Direction *PlayerSpeed); 
        Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        if (Direction.magnitude > 0.1)
            CharacterController.Move(moveDirection * PlayerSpeed * Time.deltaTime);


    }


    private void Inspect()
    {
        
    }
}
