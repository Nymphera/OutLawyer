using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceDisplay : MonoBehaviour
{   
    public MeshRenderer Mesh;
    private Material Material;
    public Evidence Evidence;
    
    void Start()
    {
        Mesh.material = Evidence.Artwork;
        
    }

    
}
