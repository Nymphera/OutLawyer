using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBoardCamera : MonoBehaviour
{[SerializeField]
    private float CameraSpeed=20f;

    
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey("w"))
        {
            position.y += CameraSpeed * Time.deltaTime;
        }
        transform.position = position;
    }
}
