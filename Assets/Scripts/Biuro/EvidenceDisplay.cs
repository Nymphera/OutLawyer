using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EvidenceDisplay : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer Renderer;

    public Evidence Evidence;
   
    private void Awake()
    {
        Evidence.Layer = 7;
    
            SpriteRender();
    }

    void SpriteRender()
    {
        Material material = new Material(Shader.Find("Standard"));
        material.mainTexture= Evidence.Artwork.texture;
        Renderer.material=material;
            
        
        //    Mesh.material =new = Evidence.Artwork;
        }
 
    
}





