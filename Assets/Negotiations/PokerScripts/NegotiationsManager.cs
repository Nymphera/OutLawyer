using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NegotiationsManager : MonoBehaviour
{
    public NegotiationState currentState;

    public static event Action<NegotiationState> OnStateChanged;
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
   
    private void Start()
    {
       
        GameEvents.current.onNegotiationsStarted += startNegotiations;

        canvas = GameObject.Find("NegotiationsCanvas");
        canvas.SetActive(false);
        
    }
    private void OnDestroy()
    {
        GameEvents.current.onNegotiationsStarted -= startNegotiations;
    }

    public void UpdateNegotiationState(NegotiationState newState)
    {

        switch (newState)
        {
            case NegotiationState.SelectType:
                HandleSelectType();
                break;
            case NegotiationState.DealCards:
                HandleDealCards();
                break;
            case NegotiationState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case NegotiationState.ComputerTurn:
                HandleComputerTurn();
                break;
            case NegotiationState.Decide:
                HandleDecide();
                break;
            case NegotiationState.Victory:
                HandleVictory();
                break;
            case NegotiationState.Lose:
                HandleLose();
                break;
        }
        currentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    private void HandleLose()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Player");
    }

    private void HandleVictory()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Player");
        canvas.SetActive(false);
    }

    private void HandleDecide()
    {
        if (!tableCards[4].isFronted)
        {
            UpdateNegotiationState(NegotiationState.ComputerTurn);
        }
        else
        {
            StartCoroutine(WaitForVeridct());
            
            
        }
    }

    private IEnumerator WaitForVeridct()
    {
        yield return new WaitForSeconds(2f);
        UpdateNegotiationState(NegotiationState.Victory);

    }

    private void HandleComputerTurn()
    {
        Debug.Log("Computer goes brrrrr");
        UpdateNegotiationState(NegotiationState.PlayerTurn);
    }

    private void HandlePlayerTurn()
    {
       
    }

    private void HandleDealCards()
    {
        startNegotiations();
    }

    private void HandleSelectType()
    {
        
    }

    private void startNegotiations()
    {
        StartCoroutine(StartNegotiations());

        UpdateNegotiationState(NegotiationState.PlayerTurn);
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
        UpdatePlayerHandValue();
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
       
        UpdatePlayerHandValue();
        UpdateNegotiationState(NegotiationState.Decide);
    }

    

    private void UpdatePlayerHandValue()
    {
        Card[] valuatedCards = new Card[cardNumber];
        for(int i = 0; i < valuatedCards.Length; i++)
        {           
                valuatedCards[i] =tableCards[i];
            Debug.Log(valuatedCards[i].MyValue);
        }
        Debug.Log(cardNumber);
        HandEvaluator playerHandEvaluator = new HandEvaluator(playerCards, valuatedCards);
        Hand playerHand = playerHandEvaluator.EvaluateHand();
        
        Debug.Log($"Player has {playerHand}");
        GameObject.Find("HandValueINT").GetComponent<TextMeshProUGUI>().text = ((int)playerHand).ToString();
        GameObject.Find("HandValueText").GetComponent<TextMeshProUGUI>().text = playerHand.ToString();
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
public enum NegotiationState
{
    SelectType,
    DealCards,
    PlayerTurn,
    ComputerTurn,
    Decide,
    Victory,
    Lose

}
