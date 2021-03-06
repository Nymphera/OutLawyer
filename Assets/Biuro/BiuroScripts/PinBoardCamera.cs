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
    [Range (0f,0.3f)]
    [SerializeField]
    private float MoveSensitivity = 0.05f;
    private float FieldOfView=40f;

    private CinemachineInputProvider InputProvider;
    private CinemachineVirtualCamera Camera;
    [SerializeField]
    private Transform cameraTransform;
    Vector3 cameraPosition;
    
  
    
    private void Awake()
    {
        InputProvider = GetComponent<CinemachineInputProvider>();
        Camera = GetComponent<CinemachineVirtualCamera>();
       // cameraTransform = Camera.VirtualCameraGameObject.transform;
        


    }

    void Update()
    {
        cameraPosition = cameraTransform.position;
       
        float x = InputProvider.GetAxisValue(0);
        float y = InputProvider.GetAxisValue(1);
        float z = InputProvider.GetAxisValue(2);
       
        if ((x != 0 || y != 0) && FieldOfView <= 35f)
        {
            MoveCamera(x, y);
        }
        if (z != 0)
        {
            ZoomCamera(z);
        }


    }

    



    public Vector3 MoveDirection(float x,float y)
    {
        Vector3 direction = Vector3.zero;
        if (y>Screen.height*(1-MoveSensitivity) &&cameraPosition.y<2.3)
        {
            direction.y += 1;
        }
        if (y < Screen.height * MoveSensitivity&&cameraPosition.y>1.5)
        {
            direction.y += -1;
        }
        if (x > Screen.width * (1-MoveSensitivity)&&cameraPosition.z>-1.25)
        {
            direction.z +=-1;
        }
        if (x < Screen.width * MoveSensitivity &&cameraPosition.z<0.3)
        {
            
            direction.z += 1;
        }

        return direction;
    }
    
    
    public void MoveCamera(float x, float y)
    {
        Vector3 Direction = MoveDirection(x, y);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, Direction*CameraSpeed + cameraTransform.position,Time.deltaTime);
       
    }
    public void ZoomCamera(float increment)
    {
         FieldOfView = Camera.m_Lens.FieldOfView;
        float target = Mathf.Clamp(FieldOfView+increment, zoomMin, zoomMax);
        Camera.m_Lens.FieldOfView = Mathf.Lerp(FieldOfView, target, Time.deltaTime * ZoomSpeed);
    }

}
