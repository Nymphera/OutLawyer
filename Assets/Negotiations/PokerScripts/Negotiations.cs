using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Negotiations :MonoBehaviour
{
    public int patienceValue=10;
    public int betValue;
    public int handValue;
    private int frontedCards=0;
    public CardSpawner cardSpawner;
    private Card[] playerCards, computerCards, tableCards;
    Slider whiteSlider, redSlider, greenSlider;
    private TextMeshProUGUI betText,handValueText,handValueInt;
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


    public void GetCards()
    {
        playerCards = new Card[2];
        computerCards = new Card[2];
        tableCards = new Card[5];
        computerCards = cardSpawner.dealCards.GetComputerHand();
        playerCards = cardSpawner.dealCards.GetPlayerHand();
        tableCards = cardSpawner.dealCards.GetTableCards();
    }
  
    public void MakeUp()
    {
        //Wysuñ jedno z nieaktywnych ¯¹dañ.Staje siê ono aktywne. Stawka maleje o wartoœæ na karcie.

        //wysuwa red offer
        //stawka--
        UpdateBet(-1);
        //cierpliwoœæ++
        UpdatePatience(1);
    }
    public void Blef()
    {
        //Zmniejsz Cierpliwoœæ o dodatkowe(1). Stawka maleje o(-1) Nie mo¿esz blefowaæ, gdy NPC ma 1 cierpliwoœci.
        UpdatePatience(-2);
        UpdateBet(-1);
        //cierpliwoœæ --
        //stawka -- 
    }
    public void Raise()
    {
        //Przebicie: Zdejmij ze sto³u ¯¹danie. Stawka roœnie o wartoœæ na karcie.

        //zdejmuje red Offer
        //stawka++
        UpdateBet(1);
        UpdatePatience(-1);
    }
    private void Call()
    {
        //Wysuñ jedn¹ z nieaktywnych Ofert. Staje siê ona aktywna. Stawka roœnie o wartoœæ na karcie.
       
        //wysuwa greenOffer
        //stawka++
        UpdateBet(1);
        UpdatePatience(-1);
    }

    private void PlayOffer()
    {
       
    }

    private void Check()
    {
        //    Sprawdzam: Pomiñ kolejkê.

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
        handValueInt=GameObject.Find("HandValueINT").GetComponent<TextMeshProUGUI>();
        handValueText= GameObject.Find("HandValueText").GetComponent<TextMeshProUGUI>();
    }
}
