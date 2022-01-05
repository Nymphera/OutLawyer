using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask Clickable;
    private NavMeshAgent Player;
    Ray myRay;
    void Start()
    {
        Player = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    { Vector3 mouse = Input.mousePosition;
        if (Input.GetKey(KeyCode.Mouse1))
        {
             myRay=Camera.main.ScreenPointToRay(mouse);

        }
        RaycastHit HitInfo;
        if(Physics.Raycast (myRay, out HitInfo,100,Clickable))
        {
            Player.SetDestination(HitInfo.point);
        }
       
    }
}
