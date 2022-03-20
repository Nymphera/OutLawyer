using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [CreateAssetMenu(fileName = "New Evidence", menuName = "Evidences", order = 1)]
[System.Serializable]

public class Evidence : ScriptableObject
    {
    [HideInInspector] public LayerMask Layer;
        public string Name;
        public string Description;
    public Sprite Artwork;
    public Orientation orientation;  
    public EvidenceType evidenceType;
    public Conection[] conection = new Conection[0];
    //public Evidence[] conectedEvidence = new Evidence[0];
       // public ConectionType[] conectionType = new ConectionType[0];

 


    public enum EvidenceType 
    { 
        Evidence,
        Location
    }
    public enum ConectionType
    {
        Yellow, // Motyw
        Red,    //Sprzecznoœæ
        Blue,   //Relacja
        Green   //Dowód
    }
   
    public enum Orientation
    {
        Vertical,
        Horizontal
    }
    [Serializable]
    public class Conection
    { public Evidence FirstEvidence;
        public Evidence ConectedEvidence;
        //  public Evidence conectionEvidence;
        public ConectionType conectionColor;
        public string Conclusion;
        //public Conclusions conclusion;
        public int conectNumber;

    }
}
public enum Conclusions
{
    Relacja1,
    Motyw2,
    Relacja3,
    Dowod4,
    Motyw5,
    Relacja6,
    Sprzecznosc79,
    Sprzecznosc8,
    Motyw10,
    Relacja11,
    Dowod12,
    Dowod13,
    Motyw14,
    Sprzecznosc15,
    Sprzecznosc16,
    Motyw17,
    Dowod18,
    Motyw19,
    Dowod20


}

