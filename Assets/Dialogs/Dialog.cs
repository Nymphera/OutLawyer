using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewDialog",menuName ="Dialogs/NewDialog",order =0)]
[System.Serializable]
public class Dialog : ScriptableObject
{
    
    public Level[] levels;
    [TextArea(3,10)]
    public string[] introductionSentences;
    public AudioClip[] introductionClips;
    public Result[] results;
    public Sprite npcImage;
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
