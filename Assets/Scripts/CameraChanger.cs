using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera OfficeCam;
    [SerializeField]
        private CinemachineFreeLook PlayerCam;
    private void Awake()
    {
        GameManager.OnGameStateChanged += ChangeCamera;
    }

    private void ChangeCamera(GameState State)
    {
        if (State == GameState.Office)
        {
            OfficeCam.Priority = 100;
            PlayerCam.Priority = 0;
        }
        if (State == GameState.Location)
        {
            OfficeCam.Priority = 0;
            PlayerCam.Priority = 100;
        }

    }
}
