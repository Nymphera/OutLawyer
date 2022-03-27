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
[System.Serializable]
[CreateAssetMenu(fileName = "DialogOption", menuName = "Dialogs/DialogOption", order = 1)]

public class DialogOption : ScriptableObject
{
    public string text;
    public CrossPoint nextCrossPoint; //ewentualnie Int
    public Strategy strategy;
}
[System.Serializable]
public class CrossPoint
{
   public DialogOption[] ConectedDialogOptions;
}
public enum Strategy
{
    ZimnaKrew,
    Podstêp,
    LuŸnaGadka,
    Profesjonalizm,
    UrokOsobisty
}
