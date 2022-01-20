using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceDisplay : MonoBehaviour
{
    public MeshRenderer Mesh;
    public Evidence Evidence;
    private Camera cam;
    private Evidence PointedEvidence = null;
    GameObject PointedObject = null;
    void Start()
    {
        cam = Camera.main;
        SpriteRender();

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





