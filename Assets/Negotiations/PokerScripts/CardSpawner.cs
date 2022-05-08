using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : DealCards
{ private GameObject cardPrefab;
    private Transform cardParent;
    public CardSpawner(GameObject cardPrefab,Transform cardParent)
    {
        this.cardPrefab = cardPrefab;
        this.cardParent = cardParent;
    }
}
