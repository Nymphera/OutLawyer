using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Offer 
{
    public int offerValue;
    public OfferType offerType;
    public string offerText;
    public bool isOfferActive;
}
public enum OfferType
{
    green,red
}