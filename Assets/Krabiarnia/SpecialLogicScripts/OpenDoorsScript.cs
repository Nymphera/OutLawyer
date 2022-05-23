using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsScript : MonoBehaviour
{
    [SerializeField]
    private int id;
    float rotationClosed;
    float rotationOpened;
    private void Start()
    {
        GameEvents.current.onDoorMouseClick+= MoveDoors;
        rotationClosed = transform.rotation.eulerAngles.y;
        rotationOpened= transform.rotation.eulerAngles.y+90;

    }
    private void OnDestroy()
    {
        GameEvents.current.onDoorMouseClick-= MoveDoors;

    }
    private void MoveDoors(int id,bool isDoorOpened)
    {
        if (isDoorOpened)
            CloseDoors(id);
        else
            OpenDoors(id);

        
    }
    private void OpenDoors(int id)
    {
        if (this.id== id)
            {
                Debug.Log("open");
                StopAllCoroutines();
                StartCoroutine(openDoors());
               
            }
        
    }
    private void CloseDoors(int id)
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
        

        float currentRotation = transform.rotation.eulerAngles.y;

        float rotation=currentRotation;
        float animationTime = 2f;
        float startTime = Time.time;
        float t =  rotation / rotationOpened;
        while (Time.time - startTime < animationTime)
        {
            rotation = Mathf.Lerp(currentRotation, rotationClosed,((Time.time - startTime)) /(Mathf.Atan(t)* animationTime));
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, rotationClosed, 0);
    }

    private IEnumerator openDoors()
    {
        
      
        
            
        float currentRotation= transform.rotation.eulerAngles.y;
        float rotation = currentRotation;
        float animationTime = 2f;
        float startTime = Time.time;
        float t =  rotation / rotationOpened;
        while (Time.time - startTime<animationTime)
        {
            rotation = Mathf.Lerp(currentRotation, rotationOpened, ((Time.time - startTime)) / (Mathf.Atan(1-t) * animationTime));
            transform.rotation = Quaternion.Euler(0,rotation,0);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0,rotationOpened,0);
    }
}
