using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealCards : DeckOfCards
{
    private Card[] playerHand;
        private Card[] computerHand;
    private Card[] tableCards;
    public DealCards()
    {
        playerHand = new Card[2];
        computerHand = new Card[2];
        tableCards = new Card[5];
    }
    public void Deal()
    {
        setUpDeck();
        getHand();
        DisplayCards();
        //EvaluateHands();
    }

    private void EvaluateHands()
    {

        HandEvaluator playerHandEvaluator = new HandEvaluator(playerHand, tableCards);
        HandEvaluator computerHandEvaluator = new HandEvaluator(computerHand, tableCards);
        Hand playerHandValue=playerHandEvaluator.EvaluateHand();
        Hand computerHandValue=computerHandEvaluator.EvaluateHand();
        Card playerHighCard = getHighCard(playerHand);
        Card computerHighCard = getHighCard(computerHand);
        Debug.Log("Player has" + playerHandValue.ToString());
        Debug.Log("Computer has" + computerHandValue.ToString());
        if (playerHandValue > computerHandValue)
        {
            Debug.Log("Player wins");
        }
        else if (playerHandValue < computerHandValue)
        {
            Debug.Log("Computer wins");
        }else if (playerHandValue == computerHandValue)
        {
            if (playerHighCard.MyValue > computerHighCard.MyValue)
            {
                Debug.Log("Player wins");
            }
            else
                Debug.Log("Computer wins");


        }
    }

    private Card getHighCard(Card[] Hand)
    {
        if (Hand[0].MyValue < Hand[1].MyValue)
            return Hand[1];
        else
            return Hand[0];
    }

    private void DisplayCards()
    {
        Debug.Log("Player Hand:");
        for (int i = 0; i < 2; i++)
        {
           Debug.Log(playerHand[i].MySuit + " " + (playerHand[i].MyValue));
            if (playerHand[i].material != null)
                Debug.Log(playerHand[i].material);
        }
        Debug.Log("Computer cards:");
        for (int j = 0; j < 2; j++)
        {
            Debug.Log(computerHand[j].MySuit+" "+ computerHand[j].MyValue);
            if (computerHand[j].material != null)
                Debug.Log(computerHand[j].material);
        }
        Debug.Log("Table Cards:");
        for (int k = 0; k < 5; k++)
        {
            Debug.Log(tableCards[k].MySuit+" "+ tableCards[k].MyValue);
            if (tableCards[k].material != null)
                Debug.Log(tableCards[k].material);
        }
    }

    private void getHand()
    {
        for(int i = 0; i < 2; i++)
        {
            playerHand[i] = getDeck[i];
        }
        for(int j = 2; j < 4; j++)
        {
            computerHand[j-2] = getDeck[j];
        }
        for(int k = 4; k < 9; k++)
        {
            tableCards[k-4] = getDeck[k];
        }
    }
    public Card[] GetPlayerHand()
    {
        return playerHand;
    }
    public Card[] GetComputerHand()
    {
        return computerHand;
    }
    public Card[] GetTableCards()
    {
        return tableCards;
    }

}
