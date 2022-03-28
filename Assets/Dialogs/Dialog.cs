using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="NewDialog",menuName ="Dialogs/NewDialog",order =0)]
[System.Serializable]
public class Dialog : ScriptableObject
{
    
    public Level[] levels;
}
[System.Serializable]
public class Level
{
    public CrossPoint[] CrossPoints;
    public DialogOption[] DialogOptions;
   
}

public enum Strategy
{
    ZimnaKrew,
    Podst�p,
    Lu�naGadka,
    Profesjonalizm,
    UrokOsobisty
}
