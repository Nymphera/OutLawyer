using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OfferDisplay : MonoBehaviour
{
    [SerializeField]
    public  Offer offer;
    private void Awake()
    {
        SetOffers();
    }

    private void SetOffers()
    {
      TextMeshProUGUI tmp=  transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tmp.text = offer.offerText;
        TextMeshProUGUI tmp2 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        tmp2.text = offer.offerValue.ToString();
        TextMeshProUGUI tmp3 = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        tmp3.text = offer.offerValue.ToString();
    }
}
