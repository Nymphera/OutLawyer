using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "New Evidence", menuName = "Evidences", order = 1)]
    public class Evidence : ScriptableObject
    {
    public LayerMask PinBoard;
        public string Name;
        public string Description;
        public Material Artwork;
    public float ArtworkXsize;
    public float ArtworkYsize;

    public GameObject Object;
   
    public EvidenceType evidenceType;
    
    public enum EvidenceType 
    { 
        Evidence,
        Location
    }
  
    
    public void ShowDescription()
    {
        Debug.Log(Description);
    }
    public void ShowType()
    {
        Debug.Log(evidenceType);

    }

}

