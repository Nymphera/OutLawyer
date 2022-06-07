using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour
{ 
    private GameObject cardPrefab;
    
    private GameObject imagePrefab;
    [SerializeField]
    private Transform playerParent,computerParent,tableParent,imageParent;
    private Card[] playerCards, computerCards, tableCards;
    private Card addedCard;
    public DealCards dealCards;
    private GameObject deckOfCards;
    
    public CardSpawner(GameObject cardPrefab,GameObject imagePrefab,Transform playerCardsParent, Transform computerCardsParent,
        Transform tableCardsParent,Transform imageParent)
    {
        this.cardPrefab = cardPrefab;
        playerParent = playerCardsParent;
        computerParent=computerCardsParent;
        tableParent = tableCardsParent;
        this.imagePrefab = imagePrefab;
        this.imageParent = imageParent;
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
    public void spawnCardImages()
    {       
        Card[] cardImages = new Card[3];
        cardImages[0] = addedCard;
        for(int i = 1; i <3; i++)
        {
            cardImages[i] = playerCards[i-1];
        }
        for(int i = 0; i < 3; i++)
        {
            RectTransform rectTransform = GameObject.Find("CardImage " + i).GetComponent<RectTransform>();           
            rectTransform.name = "Image"+cardImages[i].MySuit + cardImages[i].MyValue.ToString();
            rectTransform.anchoredPosition = new Vector2(150 * (-1 + i), 0);
           
           Sprite sprite= Resources.Load<Sprite>("Image/PlayingCards/"+cardImages[i].MySuit+cardImages[i].MyValue);
            rectTransform.GetComponent<Image>().sprite = sprite;
        }
        
    }
    public void CorrectPlayerCards(Card[] playerNewCards)
    {        
        destroyPlayerCards();
        spawnCards(playerNewCards, playerParent);
    }
    private void destroyPlayerCards()
    {
        foreach(Card card in playerCards)
        {
            Destroy(GameObject.Find(card.MySuit + card.MyValue.ToString()));
        }
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
        addedCard = dealCards.GetAddedCard();
    }
}
