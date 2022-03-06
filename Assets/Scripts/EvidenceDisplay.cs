using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceDisplay : MonoBehaviour
{
    private MeshRenderer Mesh;
    private Transform transform;
    public Evidence Evidence;
    
   
    private Camera cam;
    private Evidence PointedEvidence = null;
    private Vector3 LocationPosition;
    

    private void Awake()
    {
        
        SpriteRender();
        cam = Camera.main;

    }
   

    void SpriteRender()
        {
            Mesh.material = Evidence.Artwork;
        }
    
    
}





