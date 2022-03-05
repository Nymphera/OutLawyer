using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragLine : MonoBehaviour
{
    private PinBoardControls PinBoardControls;
    private void Awake()
    {
        PinBoardControls = new PinBoardControls();
        PinBoardControls.PinBoard.MouseLeftClick.performed += DragImage;
       
    }
    private void DragImage(InputAction.CallbackContext obj)
    {

    }
}
