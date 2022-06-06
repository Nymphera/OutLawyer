using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEvaluator : Card
{
 
    private Card[] cards;
    private HandValue handValue;
    private int spadesSum, heartSum, clubsSum, diamondSum;
    public HandEvaluator(Card[] playerCards,Card[] tableCards)
    {
        cards = new Card[playerCards.Length + tableCards.Length];
        handValue = new HandValue();
        for (int i = 0; i<cards.Length; i++)
        {
            if (i < 2)
                this.cards[i] = playerCards[i];
            else
                this.cards[i] = tableCards[i - 2];
        }
        spadesSum = 0;
        heartSum = 0;
        clubsSum = 0;
        diamondSum = 0;
    }
    public Hand EvaluateHand()
    {
        SortCards();
        GetNumberOfSuits();
        if (cards.Length >= 5)
        {
            if (KingPoker())
                return Hand.KingPoker;
            else if (Poker())
                return Hand.Poker;
            else if (FourKind())
                return Hand.FourKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (Straight())
                return Hand.Straight;
            else if (ThreeKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;
            else return Hand.Nothing;

        }
         if (cards.Length >= 4)
        {
            if (ThreeKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;
            else return Hand.Nothing;
        }
         if (cards.Length >= 3 )
        {
            if (ThreeKind())
                return Hand.ThreeKind;
            else if (OnePair())
                return Hand.OnePair;
            else return Hand.Nothing;
        }
        if (cards.Length >= 1)
        {
            if (OnePair())
                return Hand.OnePair;
            else return Hand.Nothing;
        }
        else return Hand.Nothing;
    }

    private void SortCards()
    {
        Card temporary;
        int n = cards.Length;
        while (n > 1)
        {
            for (int i = 0; i < n-1; i++)
            {
                if (cards[i].MyValue > cards[i + 1].MyValue)
                {
                    temporary = cards[i];
                    cards[i] = cards[i + 1];
                    cards[i + 1] = temporary;
                }
            }
            n--;
        }
    }

    private void GetNumberOfSuits()
    {
        for (int i = 0; i <cards.Length;i++)
        {
            if (cards[i].MySuit == Suit.Clubs)
            {
                clubsSum++;
            }
            else if (cards[i].MySuit == Suit.Diamonds)
            {
                diamondSum++;
            }
            else if (cards[i].MySuit == Suit.Hearts)
            {
                heartSum++;
            }
            else if (cards[i].MySuit == Suit.Spades)
            {
                spadesSum++;
            }
        } 
    }
    private bool KingPoker()
    {
        if (cards[0].MyValue == Value.ACE && Flush() && Straight())
            return true;

        else return false;
    }
    private bool Poker()
    {
        if (Flush() && Straight())
            return true;
        else
        return false;
    }
    private bool FourKind()
    {
        for(int i = 0; i < cards.Length-3; i++)
        {
            if(cards[i].MyValue==cards[i+1].MyValue&& cards[i].MyValue == cards[i + 2].MyValue
                && cards[i].MyValue == cards[i + 3].MyValue&& cards[i].MyValue == cards[i + 1].MyValue)
            {
                return true;
            }
        }
        return false;
    }
    private bool FullHouse()
    {
        
        for (int i = 0; i < cards.Length-4; i++)
        {
            //para przed trójk¹
            if (cards[i].MyValue == cards[i + 1].MyValue && cards[i].MyValue != cards[i + 2].MyValue)
            {
                for (int j = i + 2; j < cards.Length - 2; j++)
                {
                    if (cards[j].MyValue == cards[j + 1].MyValue && cards[i].MyValue == cards[j + 2].MyValue)
                        return true;
                }
            }
            //trójka przed par¹

            if (cards[i].MyValue == cards[i + 1].MyValue && cards[i].MyValue == cards[i + 2].MyValue)
            {
                for (int j = i + 3; j < cards.Length - 1; j++)
                {
                    if (cards[j].MyValue == cards[j + 1].MyValue) ;
                    return true;
                }
            }
        }
        return false;
    }
    private bool Flush()
    {
        if (heartSum == 5 || spadesSum == 5 || clubsSum == 5 || diamondSum == 5)
            return true;
        else
        return false;
    }
    private bool Straight()
    {
        for (int i = 0; i < cards.Length-4; i++)
        {
            if(cards[i].MyValue+1== cards[i+1].MyValue&& cards[i+1].MyValue + 1 == cards[i + 2].MyValue
                && cards[i+2].MyValue + 1 == cards[i + 3].MyValue&& cards[i+3].MyValue + 1 == cards[i + 4].MyValue)
            {
                return true;
            }
        }
        return false;
    }
    private bool ThreeKind()
    {
        for (int i = 0; i < cards.Length-2; i++)
        {
            if (cards[i].MyValue == cards[i + 1].MyValue && cards[i].MyValue == cards[i + 2].MyValue)
                return true;
        }
        return false;
    }
    private bool TwoPairs()
    {
        for (int i = 0; i < cards.Length-3; i++)
        {
            if (cards[i].MyValue == cards[i + 1].MyValue)
            {
                for (int j = i + 2; j < cards.Length - 1; j++)
                {
                    if (cards[j].MyValue == cards[j + 1].MyValue )
                        return true;
                }
            }
        }
        return false;
    }
    private bool OnePair()
    {
        for (int i = 0; i < cards.Length-1; i++)
        {
            if (cards[i].MyValue == cards[i + 1].MyValue)
                return true;
        }
        return false;
    }
}

public enum Hand
{
    Nothing=1,
    OnePair,
    TwoPairs,
    ThreeKind,
    Straight,
    Flush,   //kolor
    FullHouse,
    FourKind,
    Poker,
    KingPoker
}
public struct HandValue
{
    public int Total;
    public int HighCard;
}
