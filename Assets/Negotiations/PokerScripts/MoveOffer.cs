using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffer : MonoBehaviour
{
    public Vector3 startPosition;
    public bool wasClicked;
    public Offer offer;
    private void Awake()
    {
        startPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        offer = GetComponent<OfferDisplay>().offer;
        
    }
    public void moveUp()
    {
       
        StopAllCoroutines();
        Vector2 minPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 maxPosition = minPosition + new Vector2(0, 100);
        StartCoroutine(MoveUp(minPosition,maxPosition));        
    }
    public void moveDown()
    {
        StopAllCoroutines();
        Vector2 minPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 maxPosition = minPosition + new Vector2(0, -100);
        StartCoroutine(MoveDown(minPosition,maxPosition));       
    }
    public void moveBack()
    {
        if(!wasClicked)
        StopAllCoroutines();
        StartCoroutine(MoveBack());
    }
    private IEnumerator MoveBack()
    {
        if(!offer.isOfferActive)
        {
            Vector2 currentPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
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
        

    }
    private IEnumerator MoveDown(Vector2 minPosition,Vector2 maxPosition)
    {
        if (!offer.isOfferActive)
        {
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

    private IEnumerator MoveUp(Vector2 minPosition,Vector2 maxPosition)
    {
        if (!offer.isOfferActive)
        {
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
    public void PlayOffer()
    {
        wasClicked = true;
        StopAllCoroutines();
        Vector2 minPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 maxPosition;
        if(offer.offerType==OfferType.green)
           maxPosition = new Vector2(minPosition.x,-85);
        else
        {
            maxPosition = new Vector2(minPosition.x, 85);
        }
        StartCoroutine(MoveUp(minPosition, maxPosition));
        offer.isOfferActive = true;

    }
}
