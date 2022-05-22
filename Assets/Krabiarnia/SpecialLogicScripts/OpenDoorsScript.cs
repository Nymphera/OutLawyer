using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsScript : MonoBehaviour
{
    private bool isDoorOpened=false;
    [SerializeField]
    private int id;
    private void Start()
    {
        GameEvents.current.onDoorMouseClick+= MoveDoors;
        
    }
    public void MoveDoors(int id)
    {
        if (isDoorOpened)
            CloseDoors(id);
        else
            OpenDoors(id);

        isDoorOpened =! isDoorOpened;
    }
    public void OpenDoors(int id)
    {
        if (this.id== id)
            {
                Debug.Log("open");
                StopAllCoroutines();
                StartCoroutine(openDoors());
               
            }
        
    }
    public void CloseDoors(int id)
    {
        if (this.id == id)
        {
            Debug.Log("open");
            StopAllCoroutines();
            StartCoroutine(closeDoors());

        }
    }

    private IEnumerator closeDoors()
    {
        float rotationClosed = transform.rotation.eulerAngles.y;
        float rotationOpened;

        rotationOpened = rotationClosed - 90;

        float rotation = rotationClosed;
        float animationTime = 2f;
        float startTime = Time.time;
        while (Time.time - startTime < animationTime)
        {
            rotation = Mathf.Lerp(rotationClosed, rotationOpened, (Time.time - startTime) / animationTime);
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, rotationOpened, 0);
    }

    private IEnumerator openDoors()
    {
        
       float rotationClosed= transform.rotation.eulerAngles.y;
        float rotationOpened;
        
            rotationOpened = rotationClosed + 90;
        
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
