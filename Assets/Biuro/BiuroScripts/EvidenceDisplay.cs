using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EvidenceDisplay : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer Renderer;
    [SerializeField]
    private GameObject Plane;
    public Evidence Evidence;
    
    
    private void Awake()
    {
        
        Evidence.Layer = 7;
       
            
        
        if (Evidence.orientation.ToString() == "Vertical")
        {
            Plane.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        SpriteRender();
        SetObjectName();
        
    }
    private void SetObjectName()
    {
        transform.name = Evidence.Name;
    }
    
    void SpriteRender()
    {
        Material material = new Material(Shader.Find("Standard"));
        material = Renderer.material;
        material.mainTexture= Evidence.Artwork.texture;
       // Renderer.material.mainTexture = Evidence.Artwork.texture;
        Renderer.material=material;

    }
 
    
}





