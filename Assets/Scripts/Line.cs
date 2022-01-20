using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    Ray Ray ;
    RaycastHit Hit;
    Camera cam;
    private int LayerMask;
    
    void Start()
    {
        cam = Camera.main;
        
    }
    QueryTriggerInteraction QueryTriggerInteraction;
    
    private void Update()
    { Ray=cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(Ray,out Hit, 100))
        {
           if (Hit.transform.gameObject.layer ==7)
            {
                GameObject Evidence = Hit.transform.gameObject;
                //GameObject Pin = Evidence.\;
                if (Input.GetMouseButton(0))
                {
                    

                }






            }
          
            
          
           




        }    
    }

}
