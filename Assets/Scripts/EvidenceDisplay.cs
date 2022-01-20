using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceDisplay : MonoBehaviour
{
    public MeshRenderer Mesh;
    public Evidence Evidence;
    private Camera cam;
    private Evidence PointedEvidence = null;
    
    private void Awake()
    {
        SpriteRender();
        cam = Camera.main;
    }
  
    private void Update()
    {
        if (IsTouchingEvidence())
        {
            PointedEvidence.ShowDescription();
           
        }

    }


    void SpriteRender()
    {
        Mesh.material = Evidence.Artwork;
    }

    private bool IsTouchingEvidence()
    {

        Ray Ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        GameObject PointedObject ;

        if (Physics.Raycast(Ray, out Hit, 100))
        {
            if (Hit.transform.gameObject.layer == 7)
            {
                PointedObject = Hit.transform.gameObject;
                PointedEvidence = PointedObject.GetComponent<EvidenceDisplay>().Evidence;

                return true;

            }
            else
                return false;
        }
        else 
            return false;
    }
   
        }





