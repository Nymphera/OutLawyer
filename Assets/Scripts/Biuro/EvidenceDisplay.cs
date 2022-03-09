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
        Evidence.Layer = 7;
        Debug.Log("remember about mesh");
           // Mesh.material = Evidence.Artwork;
        }
    
    
}





