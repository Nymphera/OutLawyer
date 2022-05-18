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
    private Negotiations negotiations;
    private int animationCount=0;
    private GameObject canvas;
    private int cardNumber = 0;
    private void Awake()
    {
        NegotiationsActivator.OnNegotiationsStarted += startNegotiations;
         canvas = GameObject.Find("NegotiationsCanvas");
            canvas.SetActive(false);
    }
    private void Update()
    {       

    }
    private void startNegotiations()
    {
        StartCoroutine(StartNegotiations());
    }
    private IEnumerator StartNegotiations()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Negotiations");

        negotiations = new Negotiations(cardPrefab, playerParent, computerParent, tableParent);
       // cardSpawner = new CardSpawner(cardPrefab, playerParent, computerParent, tableParent);
       negotiations.cardSpawner.spawnCards();
        GetCards();

        StartCoroutine(AnimateDealing(computerCards,computerParent));
        yield return new WaitUntil(()=>animationCount>0);
        
        StartCoroutine(AnimateDealing(playerCards,playerParent));
        yield return new WaitUntil(() => animationCount>1);

        StartCoroutine(AnimateDealing(tableCards,tableParent));
        yield return new WaitUntil(() => animationCount>2);
       
        StartCoroutine(RotatePlayerCards());
        yield return new WaitUntil(() => animationCount>3);
        canvas.SetActive(true);
        yield return null;
        negotiations.ChooseNegotiationsType();
        negotiations.PlayerTurn();
    }

    private IEnumerator AnimateDealing(Card[] cards,Transform parent)
    {
        Vector3 v = Vector3.zero;
       
        foreach (Card card in cards)
        {
            StartCoroutine(card.Deal(parent.position + v));
            v.x += 0.05f;
            yield return new WaitForSeconds(0.3f);

        }
        animationCount++;
    }

    private IEnumerator RotatePlayerCards()
    {
        foreach(Card card in playerCards)
        {
           StartCoroutine( card.Rotate());
            yield return new WaitForSeconds(0.3f);
        }
        animationCount++;

    }
    public void RotateTableCard()
    {
        StartCoroutine(tableCards[cardNumber].Rotate());
        cardNumber++;
    }
    private void GetCards()
    {
        playerCards = new Card[2];
        computerCards = new Card[2];
        tableCards = new Card[5];
        computerCards =negotiations.cardSpawner.dealCards.GetComputerHand();
        playerCards = negotiations.cardSpawner.dealCards.GetPlayerHand();
        tableCards = negotiations.cardSpawner.dealCards.GetTableCards();
    }
}
