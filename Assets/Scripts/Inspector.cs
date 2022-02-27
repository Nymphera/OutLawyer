using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Inspector : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed=2f;

    private CinemachineInputProvider InputProvider;
    private CinemachineVirtualCamera VirtualCamera;
    private Transform cameraTransform;

    private float mousex;
    private float mousey;
    private float mousez;
    private void Awake()
    {
        InputProvider = GetComponent<CinemachineInputProvider>();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = VirtualCamera.VirtualCameraGameObject.transform;
    }
    private void Start()
    {
        
      
    }
    private void Update()
    {
        mousex = InputProvider.GetAxisValue(0);
        mousey = InputProvider.GetAxisValue(1);
        mousez = InputProvider.GetAxisValue(2);

       
        if(mousex!=0||mousey!=0||mousez!=0 )
        MoveScreen(mousex, mousey);
    }
    private Vector2 MoveDirection(float mousex,float mousey)
    {
        Vector2 direction = Vector2.zero;
        if (mousey >= Screen.height * 0.95f)
        {
            direction.y  +=1;
        }
        else if (mousey < Screen.height * 0.05f)
        {
            direction.y += -1;
        }
        if (mousex >= Screen.width * 0.95f)
        {
            direction.x += 1;
        }
        if (mousex < Screen.width * 0.05f)
        {
            direction.x += -1;
        }

        return direction;
    }
    
    public void MoveScreen(float mousex, float mousey)
    {
        Vector2 direction = MoveDirection(mousex, mousey);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraTransform.position + (Vector3)direction * moveSpeed, Time.deltaTime);
    }
   
}
