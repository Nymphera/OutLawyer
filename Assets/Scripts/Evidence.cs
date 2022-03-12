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
        public Material Artwork;
    public EvidenceType evidenceType;
    
    public Evidence[] conectedEvidence = new Evidence[0];
        public ConectionType[] conectionType = new ConectionType[0];

 


    public enum EvidenceType 
    { 
        Evidence,
        Location
    }
    public enum ConectionType
    {
        Yellow,
        Red,
        Blue,
        Green
    }

    public  class Conection
    {
        public static string String;
      //  public Evidence conectionEvidence;
        public ConectionType conectionColor;
    }
}


