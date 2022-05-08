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
    private CardSpawner cardSpawner;
    private DealCards deal = new DealCards();
    private void Awake()
    {
        NegotiationsActivator.OnNegotiationsStarted += StartNegotiations;
       
    }
 
    void StartNegotiations()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Negotiations");
        //deal.Deal();
        cardSpawner = new CardSpawner(cardPrefab, playerParent, computerParent, tableParent);
        cardSpawner.spawnCards();
    }
    
}
