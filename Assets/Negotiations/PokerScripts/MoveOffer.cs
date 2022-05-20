using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffer : MonoBehaviour
{
    private Vector3 startPosition;
    private void Awake()
    {
        startPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        Debug.Log(startPosition);
    }
    public void moveUp()
    {
       
        StopAllCoroutines();

        StartCoroutine(MoveUp());        
    }
    public void moveDown()
    {
        StopAllCoroutines();
            StartCoroutine(MoveDown());       
    }
    public void moveBack()
    {
        StopAllCoroutines();
        StartCoroutine(MoveBack());
    }
    private IEnumerator MoveBack()
    {
        
        Vector2 currentPosition= gameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 pos = startPosition;
        float startTime = Time.time;
        float animationTime = 0.3f;

        while (Time.time - startTime < animationTime)
        {
            pos = Vector3.Lerp(currentPosition, startPosition, (Time.time - startTime) / animationTime);
            gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
            yield return null;
        }
        gameObject.GetComponent<RectTransform>().anchoredPosition = startPosition;

    }
    private IEnumerator MoveDown()
    {
        
       
        Vector2 minPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 maxPosition = minPosition + new Vector2(0, -100);
        Vector2 pos = minPosition;
        float startTime = Time.time;
        float animationTime = 0.3f;
        
            while (Time.time - startTime < animationTime)
            {
                pos = Vector2.Lerp(minPosition, maxPosition, (Time.time - startTime) / animationTime);
                gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
                yield return null;
            }
            gameObject.GetComponent<RectTransform>().anchoredPosition = maxPosition;

       
    }

    private IEnumerator MoveUp()
    {
        
        

        Vector2 minPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 maxPosition = minPosition + new Vector2(0, 100);
        Vector2 pos = minPosition;
        float startTime = Time.time;
        float animationTime = 0.3f;
        
            while (Time.time - startTime < animationTime)
            {
                pos = Vector2.Lerp(minPosition, maxPosition, (Time.time - startTime) / animationTime);
                gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
                yield return null;
            }
            gameObject.GetComponent<RectTransform>().anchoredPosition = maxPosition;
      



    }

}
