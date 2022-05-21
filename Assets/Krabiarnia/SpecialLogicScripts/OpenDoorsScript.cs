using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsScript : MonoBehaviour
{ private bool isDoorOpened = false;
    [SerializeField]
    private int id;
    private void Start()
    {
        GameEvents.current.onDoorTriggerEnter += OpenDoors;
        
    }

    public void OpenDoors(int idNum)
    {
        if (id== idNum)
            {
                Debug.Log("open");
                StopAllCoroutines();
                StartCoroutine(openDoors());
                isDoorOpened = !isDoorOpened;
            }
        
    }

    private IEnumerator openDoors()
    {
        
       float rotationClosed= transform.rotation.eulerAngles.y;
        float rotationOpened;
        if (!isDoorOpened)
            rotationOpened = rotationClosed + 90;
        else 
        {
            rotationOpened = rotationClosed - 90;
        }
        float rotation = rotationClosed;
        float animationTime = 2f;
        float startTime = Time.time;
        while (Time.time - startTime<animationTime)
        {
            rotation = Mathf.Lerp(rotationClosed, rotationOpened, (Time.time - startTime) / animationTime);
            transform.rotation = Quaternion.Euler(0,rotation,0);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0,rotationOpened,0);
    }
}
