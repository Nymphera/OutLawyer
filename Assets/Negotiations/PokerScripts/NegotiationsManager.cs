using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NegotiationsManager : MonoBehaviour
{
    [SerializeField]
    public NegotiationState currentState;
    public static NegotiationsManager Instance;
    public static event Action<NegotiationState> OnStateChanged;
    [SerializeField]
    private GameObject cardPrefab,imagePrefab;
    [SerializeField]
    private Transform playerParent, computerParent, tableParent,imageParent;
    Slider whiteSlider, redSlider, greenSlider;
    private TextMeshProUGUI betText, handValueText, handValueInt;
    private Card[] playerCards, computerCards, tableCards;
    private CardSpawner cardSpawner;
    private Negotiations negotiations;
    private int animationCount=1;
    private GameObject canvas,selectTypePanel;
    private int cardNumber = 0;
    [SerializeField]
    private int patienceValue=8;
    private int betValue=0;
    private int handValue=0;

    private void Start()
    {
        Instance = this;
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }

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
        StartCoroutine(Think(0.2f));       
    }
   
    private IEnumerator Think(float time)
    {
        RotateTableCard();
        yield return new WaitForSeconds(time);
        if (!tableCards[4].isFronted&&patienceValue>0)
        {           
            UpdateNegotiationState(NegotiationState.PlayerTurn);
        }
        else
        {
            StartCoroutine(WaitForVeridct());
        }

    }
    private IEnumerator WaitForVeridct()
    {
        yield return new WaitForSeconds(4f);
        if(handValue>=betValue)
        UpdateNegotiationState(NegotiationState.Victory);
        else if (handValue < betValue)
        {
            UpdateNegotiationState(NegotiationState.Lose);
        }
    }

    private void HandleComputerTurn()
    {
        Debug.Log("Computer goes brrrrr");
        UpdateNegotiationState(NegotiationState.PlayerTurn);
    }

    private void HandlePlayerTurn()
    {
        selectTypePanel.SetActive(false);
    }

    private void HandleDealCards()
    {
        
    }

    private void HandleSelectType()
    {
        selectTypePanel.SetActive(true);
    }

    private void startNegotiations()
    {
        StartCoroutine(StartNegotiations());

        UpdateNegotiationState(NegotiationState.DealCards);
    }
    private IEnumerator StartNegotiations()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Negotiations");

        
        cardSpawner = new CardSpawner(cardPrefab,imagePrefab, playerParent, computerParent, tableParent,imageParent);
       
       cardSpawner.spawnCards();
        GetCards();
       

      //  StartCoroutine(AnimateDealing(computerCards,computerParent));
       // yield return new WaitUntil(()=>animationCount>0);
        
        StartCoroutine(AnimateDealing(playerCards,playerParent));
        yield return new WaitUntil(() => animationCount>1);

        StartCoroutine(AnimateDealing(tableCards,tableParent));
        yield return new WaitUntil(() => animationCount>2);
       
        StartCoroutine(RotatePlayerCards());
        yield return new WaitUntil(() => animationCount>3);
        canvas.SetActive(true);
        GetRefernces();
        yield return null;
        UpdateNegotiationState(NegotiationState.SelectType);
       
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
    private void RotateTableCard()
    {
        
        StartCoroutine(tableCards[cardNumber].Rotate());
        cardNumber++;
       
        UpdatePlayerHandValue();
        
    }

    

    private void UpdatePlayerHandValue()
    {
        Card[] valuatedCards = new Card[cardNumber];
        for(int i = 0; i < valuatedCards.Length; i++)
        {           
                valuatedCards[i] =tableCards[i];
           
        }
        
        HandEvaluator playerHandEvaluator = new HandEvaluator(playerCards, valuatedCards);
        Hand playerHand = playerHandEvaluator.EvaluateHand();
        
        
        handValueInt.text = (handValue+((int)playerHand)).ToString();
        handValueText.text = playerHand.ToString();
       // handValue += ((int)playerHand);
    }

    private void GetCards()
    {
        playerCards = new Card[2];
        computerCards = new Card[2];
        tableCards = new Card[5];
        computerCards =cardSpawner.dealCards.GetComputerHand();
        playerCards = cardSpawner.dealCards.GetPlayerHand();
        tableCards = cardSpawner.dealCards.GetTableCards();
    }

    public void MakeUp(RectTransform rectTransform)
    {
        if (currentState == NegotiationState.PlayerTurn)
        {
            //Wysuń jedno z nieaktywnych Żądań.Staje się ono aktywne. Stawka maleje o wartość na karcie.
            Offer offer = rectTransform.GetComponent<OfferDisplay>().offer;
            if (!offer.isOfferActive&&betValue-offer.offerValue>=0)
            {
                //MoveOffer.PlayOffer();
                //wysuwa greenOffer
                rectTransform.GetComponent<MoveOffer>().PlayOffer();

                //stawka++
                UpdateBet(-offer.offerValue);

                UpdatePatience(-1);

                UpdateNegotiationState(NegotiationState.Decide);
                //wysuwa red offer
                //stawka--
            }
        }
    }
    public void Blef()
    {
        if (currentState == NegotiationState.PlayerTurn)
        {
            //Zmniejsz Cierpliwość o dodatkowe(1). Stawka maleje o(-1) Nie możesz blefować, gdy NPC ma 1 cierpliwości.
            if(betValue -1 >= 0)
            {
                UpdatePatience(-2);
                UpdateBet(-1);
                //cierpliwość --
                //stawka -- 
                UpdateNegotiationState(NegotiationState.Decide);
            }           
        }
    }
    public void Raise(RectTransform rectTransform)
    {
        //Przebicie: Zdejmij ze stołu Żądanie. Stawka rośnie o wartość na karcie.
        if (currentState == NegotiationState.PlayerTurn)
        {
            Offer offer = rectTransform.GetComponent<OfferDisplay>().offer;
            if (offer.isOfferActive)
            {
                //zdejmuje red Offer
                UpdateBet(offer.offerValue);
                //stawka++
                UpdatePatience(-1);
                offer.isOfferActive = false;
                rectTransform.GetComponent<MoveOffer>().moveBack();

                UpdateNegotiationState(NegotiationState.Decide);
            }
        }

    }
    public void Call(RectTransform rectTransform)
    {
        //Wysuń jedną z nieaktywnych Ofert. Staje się ona aktywna. Stawka rośnie o wartość na karcie.
        if (currentState == NegotiationState.PlayerTurn)
        {
            Offer offer = rectTransform.GetComponent<OfferDisplay>().offer;
            if (!offer.isOfferActive)
            {
                //MoveOffer.PlayOffer();
                //wysuwa greenOffer
                rectTransform.GetComponent<MoveOffer>().PlayOffer();

                //stawka++
                UpdateBet(offer.offerValue);

                UpdatePatience(-1);

                UpdateNegotiationState(NegotiationState.Decide);
            }
        }      
    }
    public void Check()
    {
        
        if (currentState == NegotiationState.PlayerTurn)
        {
            UpdateNegotiationState(NegotiationState.Decide);        
        }
        //    Sprawdzam: Pomiń kolejkę.

        //nic nie rób     

    }
    public void ShowBlefEffects()
    {
        ShowBetChange(-1);
        ShowPatienceChange(-2);
    }
    public void HideBlefEffects()
    {
        HideBetChange();
        HidePatienceChange();
    }
    public void ShowClickEffects(RectTransform rectTransform)
    {        
        
       Offer offer=rectTransform.GetComponent<OfferDisplay>().offer;
        if (offer.offerType == OfferType.green&&!offer.isOfferActive)
        {
            rectTransform.GetComponent<MoveOffer>().moveUp();
            ShowPatienceChange(-1);
            ShowBetChange(offer.offerValue);
        }
        else if (offer.offerType == OfferType.red)
        {
            
            rectTransform.GetComponent<MoveOffer>().moveDown();
            ShowPatienceChange(-1);
            if (!offer.isOfferActive)
            {
                rectTransform.GetComponent<MoveOffer>().moveDown();
                ShowBetChange(-offer.offerValue);
            }
            else
            {               
                rectTransform.GetComponent<MoveOffer>().moveUp();                
                ShowBetChange(offer.offerValue);
            }
           
        }
        
       
    }
    public void HideClickEffects(RectTransform rectTransform)
    {
        rectTransform.GetComponent<MoveOffer>().moveBack();
        HideBetChange();
        HidePatienceChange();
    }

    private void ShowBetChange(int value)
    {
        if (betValue + value < 0)
        {
            Debug.Log("Stawka nie może być mniejsza od zera");
        }
        if (value > 0)
        {
            betText.color = Color.red;
            betText.text = (betValue + value).ToString()+ "↑";
            
        }
        else if (value < 0)
        {
            betText.color = Color.green;
            betText.text = (betValue + value).ToString()+ "↓";
        }
    }
    private void HideBetChange()
    {
        betText.color = Color.white;
        betText.text = betValue.ToString();
    }

    private void ShowPatienceChange(int value)
    {
        if (value > 0)
        {
            greenSlider.value = patienceValue + value;
        }
        else if (value < 0)
        {
            whiteSlider.value = patienceValue + value;
        }
    }
    private void HidePatienceChange()
    {
        whiteSlider.value = patienceValue;
        redSlider.value = whiteSlider.value;
        greenSlider.value = whiteSlider.value;
    }
    public void UpdatePlayerCards(RectTransform rectTransform)
    {for(int i = 0; i < 2; i++)
        {
            if (rectTransform.name == playerCards[i].MySuit + playerCards[i].MyValue.ToString())
            {
                playerCards[i] = cardSpawner.dealCards.GetAddedCard();
                cardSpawner.CorrectPlayerCards(playerCards);
            }
        }
        UpdateNegotiationState(NegotiationState.PlayerTurn);
    }
    public void AsWRekawie()
    {       
        cardSpawner.spawnCardImages();
    }
    public void UczciweTasowanie()
    {
        handValue = 1;
        UpdatePlayerHandValue();
        UpdateNegotiationState(NegotiationState.PlayerTurn);
    }
    public void PodejrzyjKarte()
    {
       
        RotateTableCard();
        UpdateNegotiationState(NegotiationState.PlayerTurn);
        
    }
    private void UpdatePatience(int value)
    {
        patienceValue += value;

        whiteSlider.value = patienceValue;
        redSlider.value = whiteSlider.value;
        greenSlider.value = whiteSlider.value;
    }
    private void UpdateBet(int value)
    {
        betValue += value;
        betText.text = betValue.ToString();
    }

    public void GetRefernces()
    {
        whiteSlider = GameObject.Find("Slider_White").GetComponent<Slider>();
        greenSlider = GameObject.Find("Slider_Green").GetComponent<Slider>();
        redSlider = GameObject.Find("Slider_Red").GetComponent<Slider>();
        betText = GameObject.Find("BetValueINT").GetComponent<TextMeshProUGUI>();
        handValueInt = GameObject.Find("HandValueINT").GetComponent<TextMeshProUGUI>();
        handValueText = GameObject.Find("HandValueText").GetComponent<TextMeshProUGUI>();
        selectTypePanel = GameObject.Find("SelectType");
       
        whiteSlider.maxValue = patienceValue;
        greenSlider.maxValue = patienceValue;
        redSlider.maxValue = patienceValue;
        
    }
}
public enum NegotiationsType
{
    Null = 0,
    UczciweTasowanie,
    PodjerzyjKarte,
    AsWRękawie
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
