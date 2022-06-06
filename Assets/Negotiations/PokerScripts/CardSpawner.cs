using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{ [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private Transform playerParent,computerParent,tableParent;
    private Card[] playerCards, computerCards, tableCards;
    public DealCards dealCards;
    private GameObject deckOfCards;
    public CardSpawner(GameObject cardPrefab,Transform playerCardsParent, Transform computerCardsParent,
        Transform tableCardsParent)
    {
        this.cardPrefab = cardPrefab;
        playerParent = playerCardsParent;
        computerParent=computerCardsParent;
        tableParent = tableCardsParent;

        dealCards = new DealCards();
        playerCards = new Card[2];
        computerCards = new Card[2];
        tableCards = new Card[5];
        deckOfCards = GameObject.Find("DeckOfCards");
    }
    public void spawnCards()
    {
        

        DealCards();
        spawnCards(playerCards, playerParent);
       // yield return null;
        //spawnCards(computerCards, computerParent);
       // yield return null;
        spawnCards(tableCards, tableParent);




    }

    private void spawnCards(Card[] Cards,Transform parent)
    {
        Vector3 v=Vector3.zero;
        Quaternion rotation;
        Vector3 spawnPosition = deckOfCards.transform.position;
        spawnPosition.y += 0.01f;
        Vector3 endPosition;
        foreach (Card card in Cards)
        {
            if (card.isFronted == false) 
            {
                rotation = Quaternion.Euler(0, 0, 180);
            }
                else
            {
                rotation = Quaternion.identity;
            }
            endPosition = parent.position + v;

            GameObject cardObject = Instantiate(cardPrefab,spawnPosition,rotation, parent).gameObject;
            cardObject.name = card.MySuit.ToString() + card.MyValue;
            cardObject.GetComponent<MeshRenderer>().material = card.material;

            v.x += 0.05f;
        }
        
    }

    private void DealCards()
    {
        dealCards.Deal();
        playerCards = dealCards.GetPlayerHand();
        computerCards = dealCards.GetComputerHand();
        tableCards = dealCards.GetTableCards();
    }
}
