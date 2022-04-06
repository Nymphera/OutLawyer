using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="NewDialog",menuName ="Dialogs/NewDialog",order =0)]
[System.Serializable]
public class Dialog : ScriptableObject
{
    
    public Level[] levels;
    [TextArea(3,10)]
    public string[] introductionSentences;
    public AudioClip[] introductionClips;
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
    Podstêp,
    LuŸnaGadka,
    Profesjonalizm,
    UrokOsobisty
}
