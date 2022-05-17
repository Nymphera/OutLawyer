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
        NegotiationsActivator.OnNegotiationsStarted += startNegotiations;
       
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

        cardSpawner = 
           new CardSpawner(cardPrefab, playerParent, computerParent, tableParent);
        
        cardSpawner.spawnCards();
        GetCards();
        StartCoroutine(AnimateDealing());
        yield return new WaitForSeconds(2f);
        RotatePlayerCards();
    }

    private IEnumerator AnimateDealing()
    {
        Vector3 v = Vector3.zero;
       
        foreach (Card card in computerCards)
        {
            StartCoroutine(card.Deal(computerParent.position + v));
            v.x += 0.05f;
            yield return new WaitForSeconds(0.3f);

        }
        yield return null;
        v = Vector3.zero;

        foreach (Card card in playerCards)
        {
            StartCoroutine(card.Deal(playerParent.position + v));
            yield return new WaitForSeconds(0.3f);
            v.x += 0.05f;
        }
        yield return null;
        

        v = Vector3.zero;
        foreach (Card card in tableCards)
        {
            StartCoroutine(card.Deal(tableParent.position + v));
            v.x += 0.05f;
            yield return new WaitForSeconds(0.3f);

        }
    }

    private void RotatePlayerCards()
    {
        foreach(Card card in playerCards)
        {
           StartCoroutine( card.Rotate());
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
