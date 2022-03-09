using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "New Evidence", menuName = "Evidences", order = 1)]
    public class Evidence : ScriptableObject
    {
    [HideInInspector] public LayerMask Layer;
        public string Name;
        public string Description;
        public Material Artwork;
 

    public EvidenceType evidenceType;
    
    public enum EvidenceType 
    { 
        Evidence,
        Location
    }
  
}

