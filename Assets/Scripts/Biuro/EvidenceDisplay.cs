using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EvidenceDisplay : MonoBehaviour
{
    private MeshRenderer Mesh;

    public Evidence Evidence;
    
   
 
  

    private void Awake()
    {
    
        
        SpriteRender();
      

    }


    void SpriteRender()
        {
            Mesh.material = Evidence.Artwork;
        }
    
    
}





