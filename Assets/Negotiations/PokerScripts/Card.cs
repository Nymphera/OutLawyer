using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card 
{
    
    public enum Suit
    {
        Clubs,
        Hearts,
        Spades,
        Diamonds
        
    }
    public enum Value
    {
        TWO = 2,THREE,FOUR,FIVE,SIX,SEVEN,
        EIGHT,NINE,TEN,JACK,QUEEN,KING,ACE
    }
    public Suit MySuit { get; set; }
    public Value MyValue { get; set; }
    public bool isFronted { get; set; }
    public Material material;
    public IEnumerator Rotate()
    {
        isFronted = !isFronted;
        GameObject cardObject = GameObject.Find(MySuit.ToString() + MyValue.ToString());
       float rotationZ;
        
        float animationTime = 0.5f;
        float startTime = Time.time;
        Vector3 rotation;
        while (Time.time - startTime < animationTime)
        {
            rotationZ=Mathf.Lerp(-180, 0, (Time.time - startTime)/animationTime);
            
            cardObject.transform.rotation= Quaternion.Euler(0, 0, rotationZ);
            yield return null;
            
        }
            
        cardObject.transform.Rotate(0, 0, 0);
       
    } 
    public IEnumerator Deal(Vector3 endPosition)
    {
        GameObject cardObject = GameObject.Find(MySuit.ToString() + MyValue.ToString());
        float animationDuration = 0.3f;
        float startTime = Time.time;
        Vector3 spawnPosition= cardObject.transform.position;
        Vector3 pos = cardObject.transform.position;
        while (pos != endPosition)
        {
            pos = Vector3.Lerp(spawnPosition, endPosition, (Time.time - startTime) / animationDuration);
            cardObject.transform.position = pos;
            yield return null;

            
        }
        cardObject.transform.position = endPosition;
        
    }
    
}
