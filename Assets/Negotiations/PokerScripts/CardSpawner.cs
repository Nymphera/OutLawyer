using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
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
    public void spawnCards()
    {
        DealCards();

        spawnCards(playerCards,playerParent);
        spawnCards(computerCards,computerParent);
        spawnCards(tableCards,tableParent);

         
        
    }

    private void spawnCards(Card[] Cards,Transform parent)
    {
        Vector3 v=Vector3.zero;
        foreach(Card card in Cards)
        {
            GameObject cardObject = Instantiate(cardPrefab,parent.position+v,Quaternion.identity, parent).gameObject;
            cardObject.name = card.MySuit.ToString() + card.MyValue;
            cardObject.GetComponent<MeshRenderer>().material = card.material;
            v.x += 0.1f;
        }
        
    }

    private void DealCards()
    {
        deal.Deal();
        playerCards = deal.GetPlayerHand();
        computerCards = deal.GetComputerHand();
        tableCards = deal.GetTableCards();
    }
}
