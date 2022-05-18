using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffer : MonoBehaviour
{
    private bool isAnimating=false;
    
   
    public void moveUp()
    {
            StartCoroutine(MoveUp());        
    }
    public void moveDown()
    {
            StartCoroutine(MoveDown());       
    }
 
    private IEnumerator MoveDown()
    {
        
        Vector2 minPosition = gameObject.GetComponent<RectTransform>().position;
        Vector2 maxPosition = minPosition + new Vector2(0, -240);
        Vector2 pos = minPosition;
        float startTime = Time.time;
        float animationTime = 0.3f;
        
            while (Time.time - startTime < animationTime)
            {
                pos = Vector2.Lerp(minPosition, maxPosition, (Time.time - startTime) / animationTime);
                gameObject.GetComponent<RectTransform>().position = pos;
                yield return null;
            }
            gameObject.GetComponent<RectTransform>().position = maxPosition;

       
    }

    private IEnumerator MoveUp()
    {
        
        
        Vector2 minPosition = gameObject.GetComponent<RectTransform>().position;
        Vector2 maxPosition = minPosition + new Vector2(0, 240);
        Vector2 pos = minPosition;
        float startTime = Time.time;
        float animationTime = 0.3f;
        
            while (Time.time - startTime < animationTime)
            {
                pos = Vector2.Lerp(minPosition, maxPosition, (Time.time - startTime) / animationTime);
                gameObject.GetComponent<RectTransform>().position = pos;
                yield return null;
            }
            gameObject.GetComponent<RectTransform>().position = maxPosition;
      



    }

}
