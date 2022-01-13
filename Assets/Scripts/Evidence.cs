using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "New Evidence", menuName = "Evidences", order = 1)]
    public class Evidence : ScriptableObject
    {
        public string EvidenceName;
        public string Description;
        public Material Artwork;
        

    }

