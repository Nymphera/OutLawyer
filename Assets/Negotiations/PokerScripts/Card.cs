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
    public Material material;
}
