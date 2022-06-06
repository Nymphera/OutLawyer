using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NegotiationsManager : MonoBehaviour
{
    public NegotiationState currentState;

    public static event Action<NegotiationState> OnStateChanged;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private Transform playerParent, computerParent, tableParent;
    Slider whiteSlider, redSlider, greenSlider;
    private TextMeshProUGUI betText, handValueText, handValueInt;
    private Card[] playerCards, computerCards, tableCards;
    private CardSpawner cardSpawner;
    private Negotiations negotiations;
    private int animationCount=0;
    private GameObject canvas;
    private int cardNumber = 0;
    private int patienceValue=10;
    private int betValue=0;
    private int handValue=1;

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
        yield return new WaitForSeconds(4f);
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
        cardSpawner = new CardSpawner(cardPrefab, playerParent, computerParent, tableParent);
       
       cardSpawner.spawnCards();
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
        GetRefernces();
        yield return null;
        ChooseNegotiationsType();
       
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
        UpdateNegotiationState(NegotiationState.Decide);
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
        
        
        handValueInt.text = ((int)playerHand).ToString();
        handValueText.text = playerHand.ToString();
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
    public IEnumerator ChooseNegotiationsType()
    {
        //wy�wietla trzy opcje

        yield return null; //wati until przycisk zosta� wci�ni�ty

        //  znikaj� trzy opcje

        // klikni�cie przycisku w��cza przej�cie do nast�pnej funkcji
    }


    public void MakeUp()
    {
        //Wysu� jedno z nieaktywnych ��da�.Staje si� ono aktywne. Stawka maleje o warto�� na karcie.

        //wysuwa red offer
        //stawka--
        UpdateBet(-1);
        //cierpliwo��++
        UpdatePatience(1);
    }
    public void Blef()
    {
        //Zmniejsz Cierpliwo�� o dodatkowe(1). Stawka maleje o(-1) Nie mo�esz blefowa�, gdy NPC ma 1 cierpliwo�ci.
        UpdatePatience(-2);
        UpdateBet(-1);
        //cierpliwo�� --
        //stawka -- 
    }
    public void Raise()
    {
        //Przebicie: Zdejmij ze sto�u ��danie. Stawka ro�nie o warto�� na karcie.

        //zdejmuje red Offer
        //stawka++
        UpdateBet(1);
        UpdatePatience(-1);
    }
    public void Call(RectTransform rectTransform)
    {
        //Wysu� jedn� z nieaktywnych Ofert. Staje si� ona aktywna. Stawka ro�nie o warto�� na karcie.
        
        Offer offer = rectTransform.GetComponent<OfferDisplay>().offer;
        if(offer.isOfferActive == false)
        {
            //MoveOffer.PlayOffer();
            //wysuwa greenOffer
            rectTransform.GetComponent<MoveOffer>().PlayOffer();
          
            //stawka++
            UpdateBet(offer.offerValue);
            
            UpdatePatience(-1);
            RotateTableCard();
        }
       
    }
    public void ShowCallEffects(RectTransform rectTransform)
    {
        //pokazuje karte
        rectTransform.GetComponent<MoveOffer>().moveUp();
       Offer offer=rectTransform.GetComponent<OfferDisplay>().offer;
        //patience
        ShowPatienceChange(-1);
        //jak w d� to bia�y spada
        //jak w g�r� to zielony ro�nie

        //bet
        ShowBetChange(offer.offerValue);
        //jak w d� to stawka na czerwono z jakim� znaczkiem w d�
        //jak w g�re to na zielono i strza�ka do g�ry
    }
    public void HideCallEffects(RectTransform rectTransform)
    {
        rectTransform.GetComponent<MoveOffer>().moveBack();
        HideBetChange();
        HidePatienceChange();
    }

    private void ShowBetChange(int value)
    {
        if (value > 0)
        {
            betText.color = Color.red;
            betText.text = (betValue + value).ToString();
        }
        else if (value < 0)
        {
            betText.color = Color.green;
            betText.text = (betValue + value).ToString();
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

    private void Check()
    {
        //    Sprawdzam: Pomi� kolejk�.

        //nic nie r�b     

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
    }
}
public enum NegotiationsType
{
    Null = 0,
    UczciweTasowanie,
    PodjerzyjKarte,
    AsWR�kawie
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
