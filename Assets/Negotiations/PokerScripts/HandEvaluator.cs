using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEvaluator : Card
{
 
    private Card[] cards;
    private HandValue handValue;

    public HandEvaluator(Card[] cards)
    {
        handValue = new HandValue(); 
    }
}
public enum Hand
{

}
public struct HandValue
{
    public int Total;
    public int HighCard;
}
