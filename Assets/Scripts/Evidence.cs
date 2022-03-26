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
   // public Line.Conection[] conection = new Line.Conection[0];
    public IsConectedTo[] Conections = new IsConectedTo[0];

 


    public enum EvidenceType 
    { 
        Evidence,
        Location
    }
    
   
    public enum Orientation
    {
        Vertical,
        Horizontal
    }
    [Serializable]
    public class IsConectedTo
    {
        public Evidence conected;
        public ConectionType ConectionType;
       [HideInInspector] public string Conclusion;
        public int conectNumber;
       
    }
    
}


