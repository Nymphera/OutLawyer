using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NegotiationsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private Transform playerParent, computerParent, tableParent;
    private Card[] playerCards, computerCards, tableCards;
    private CardSpawner cardSpawner;
    
    private void Awake()
    {
        NegotiationsActivator.OnNegotiationsStarted += StartNegotiations;
       
    }
 
    void StartNegotiations()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Negotiations");
        
        cardSpawner = new CardSpawner(cardPrefab, playerParent, computerParent, tableParent);
        cardSpawner.spawnCards();
        GetCards();
        RotatePlayerCards();
    }

    private void RotatePlayerCards()
    {
        foreach(Card card in playerCards)
        {
            card.Rotate();
        }
    }

    private void GetCards()
    {
        playerCards = new Card[2];
        computerCards = new Card[2];
        tableCards = new Card[5];
        computerCards =cardSpawner.deal.GetComputerHand();
        playerCards = cardSpawner.deal.GetPlayerHand();
        tableCards = cardSpawner.deal.GetTableCards();
    }
}
