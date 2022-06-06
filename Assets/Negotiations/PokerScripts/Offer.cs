using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offer 
{
    public int offerValue;
    public OfferType offerType;
    public string offerText;
}
public enum OfferType
{
    green,red
}