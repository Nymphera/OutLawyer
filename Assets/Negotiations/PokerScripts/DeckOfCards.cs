using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckOfCards : Card
{
    private int NumberOfCards=52;
    Card[] deck;
    public DeckOfCards()
    {
        deck = new Card[NumberOfCards];
    }
    public Card[] getDeck { get { return deck; } }
    public void setUpDeck()
    {
        int i = 0;
        foreach(Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach(Value value in Enum.GetValues(typeof(Value)))
            {
                deck[i] = new Card { MySuit = suit,MyValue=value };
                i++;
                
                //{ MySuit = suit, MyValue = value };
            }
        }
        ShuffleCards();
    }
    void ShuffleCards()
    {
        System.Random random = new System.Random();
        Card temporary;
        int shuffleTimes = 1000;
        for(int i=0;i<shuffleTimes;i++)
        for(int j = 0; j < NumberOfCards; j++)
        {
            int secondCard = random.Next(13);
            temporary = deck[j];
            deck[j] = deck[secondCard];
            deck[secondCard] = temporary;
        }

    }
}
