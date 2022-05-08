using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : DealCards
{ private GameObject cardPrefab;
    private Transform playerParent,computerParent,tableParent;
    private Card[] playerCards, computerCards, tableCards;
    private DealCards deal;

    public CardSpawner(GameObject cardPrefab,Transform playerCardsParent, Transform computerCardsParent, Transform tableCardsParent)
    {
        this.cardPrefab = cardPrefab;
        playerParent = playerCardsParent;
        computerParent=computerCardsParent;
        tableParent = tableCardsParent;
        deal = new DealCards();
        playerCards = new Card[2];
        computerCards = new Card[2];
        tableCards = new Card[5];
    }
    private void spawnCards()
    {
       
        
    }
    private void DealCards()
    {
        deal.Deal();
        playerCards = deal.GetPlayerHand();
        computerCards = deal.GetComputerHand();
        tableCards = deal.GetTableCards();
    }
}
