using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiationsActivator : MonoBehaviour
{
    public static event Action OnNegotiationsStarted;
    private void Start()
    {
        StartNegotiations();
    }

    private void StartNegotiations()
    {
        OnNegotiationsStarted();
        Debug.Log("Start Negotiations");
    }
}
