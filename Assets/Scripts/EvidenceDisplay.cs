using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceDisplay : MonoBehaviour
{
    public MeshRenderer Mesh;
    public Transform transform;
    public Evidence Evidence;
    
   
    private Camera cam;
    public Evidence PointedEvidence = null;
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





