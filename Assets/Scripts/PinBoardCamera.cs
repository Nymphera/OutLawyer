using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PinBoardCamera : MonoBehaviour
{
    
    
    [SerializeField]
    private float CameraSpeed=2f;  
    [SerializeField]
    private float ZoomSpeed=3f;
    [SerializeField]
    private float zoomMin = 40f;
    [SerializeField]
    private float zoomMax = 10f;

    [SerializeField]
    private Vector3 FollowOffset;

    private CinemachineInputProvider InputProvider;
    private CinemachineVirtualCamera Camera;
    private Transform cameraTransform; 
  
    
    private void Awake()
    {
        InputProvider = GetComponent<CinemachineInputProvider>();
        Camera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = Camera.VirtualCameraGameObject.transform;

        
       
    }

    void Update()
    {
        float x = InputProvider.GetAxisValue(0);
        float y = InputProvider.GetAxisValue(1);
        float z = InputProvider.GetAxisValue(2);

        if (x != 0 || y != 0)
        {
            MoveCamera(x, y);
        }
        if (z != 0)
        {
            ZoomCamera(z);
        }
        
       
    }

   
    
    
    
    public Vector2 MoveDirection(float x,float y)
    {
        Vector2 direction = Vector2.zero;
        if (y>Screen.height*0.95f)
        {
            direction.y += 1;
        }
        if (y < Screen.height * 0.05f)
        {
            direction.y += -1;
        }
        if (x > Screen.width * 0.95f)
        {
            direction.x +=-1;
        }
        if (x < Screen.width * 0.05f)
        {
            direction.x += 1;
        }

        return direction;
    }
    
    
    public void MoveCamera(float x, float y)
    {
        Vector2 Direction = MoveDirection(x, y);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, (Vector3)Direction*CameraSpeed + cameraTransform.position,Time.deltaTime);
        Debug.Log(Direction);
    }
    public void ZoomCamera(float increment)
    {
        float FieldOfView = Camera.m_Lens.FieldOfView;
        float target = Mathf.Clamp(FieldOfView+increment, zoomMin, zoomMax);
        Camera.m_Lens.FieldOfView = Mathf.Lerp(FieldOfView, target, Time.deltaTime * ZoomSpeed);
    }

}
