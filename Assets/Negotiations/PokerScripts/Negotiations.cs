using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Negotiations :MonoBehaviour
{
    public int patienceValue;
    public int betValue;
    public int handValue;
    public CardSpawner cardSpawner;
    private Card[] playerCards, computerCards, tableCards;

    private void Awake()
    {
        NegotiationsManager.OnStateChanged += OnStateChanged;
    }
    private void OnDestroy()
    {
        NegotiationsManager.OnStateChanged -= OnStateChanged;
    }
    private void OnStateChanged(NegotiationState obj)
    {
        Debug.Log("im listening");
    }

    public Negotiations(GameObject cardPrefab, Transform playerParent, 
                         Transform computerParent,    Transform tableParent)
    {
        cardSpawner = new CardSpawner(cardPrefab, playerParent, computerParent, tableParent);
        GetCards();   
    }
    public IEnumerator ChooseNegotiationsType()
    {
        //wyœwietla trzy opcje
        
        yield return null; //wati until przycisk zosta³ wciœniêty
        
        //  znikaj¹ trzy opcje
        
        // klikniêcie przycisku w³¹cza przejœcie do nastêpnej funkcji
    }
    public void PlayerTurn()
    {
        EvaluateHand();

        // mo¿na klikaæ w przyciski 

        ActionResults();
    }
        public void ActionResults()
    {
        //blokowanie w³¹czenia przycisków
    }
    private void EvaluateHand()
    {
        
    }

    private void GetCards()
    {
        playerCards = new Card[2];
        computerCards = new Card[2];
        tableCards = new Card[5];
        computerCards = cardSpawner.dealCards.GetComputerHand();
        playerCards = cardSpawner.dealCards.GetPlayerHand();
        tableCards = cardSpawner.dealCards.GetTableCards();
    }
    private void RotateNextCard()
    {

    }
    private void MakeUp()
    {
        //wysuwa red offer
        //stawka--
    }
    private void Blef()
    {
        //cierpliwoœæ -- stawka --
    }
    private void Raise()
    {
        //zdejmuje red Offer
        //stawka++
    }
    private void Call()
    {
        //wysuwa greenOffer
        //stawka++
    }
    private void Check()
    {
       //nic nie rób     
    }
    public void AsWRekawie()
    {
        patienceValue = 0;
        betValue = 0;
        handValue = 0;
    }
    public void UczciweTasowanie()
    {
        patienceValue = 0;
        betValue = 0;
        handValue = 0;
    }
    public void PodejrzyjKarte()
    {
        patienceValue = 0;
        betValue = 0;
        handValue = 0;
    }
}
public enum NegotiationsType
{   Null=0,
    UczciweTasowanie,
    PodjerzyjKarte,
    AsWRêkawie
}