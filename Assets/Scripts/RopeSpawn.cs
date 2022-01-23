using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject PartPrefab, ParentObject;
    [SerializeField]
    bool reset, spawn, snapLast;
    [SerializeField]
    [Range(1, 100)]
    float Length = 1;
    [SerializeField]
    private float PartDistance=0.21f;
    [SerializeField]
    GameObject FirstPin, SecondPin = null;

    private Vector3 MousePos;



    private void Update()
    {

        
        if (reset == true)
        {
            DestroyRope();
            reset = false;
        }
        if (spawn == true)
        {//spawn rope po wciœniêciu guzika 
            spawn = true;
            SpawnRope();
            spawn = false;
           
        }
        if (Input.GetMouseButton(1) )
        { 
            FirstPin = PinPosition();
        
        }
        if (Input.GetMouseButton(0))
        {
            SecondPin = PinPosition();
        }
    }
   public GameObject PinPosition()
    {
        GameObject Temporary;
        Ray Ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(Ray, out Hit, 100))
        {
            if (Hit.transform.gameObject.layer == 7)
            {
               
                Temporary = Hit.transform.gameObject;
                print("heh");
                return Temporary.transform.gameObject;
                
            }
            else
            {
                
                return null;
            }
        }
        else
        {
            return null;
        }
            

    }
    public void DestroyRope()
    {
       foreach(GameObject Temporary in GameObject.FindGameObjectsWithTag("Rope"))
        {
            Destroy(Temporary);
        }
    }
    public void SpawnRope()
    {
        //tu trzeba daæ odleg³oœæ miêdzy pinami na pewno
        float Length=Vector3.Distance(FirstPin.transform.position,SecondPin.transform.position);
        int count = (int)(Length / PartDistance);
        Debug.Log(count);




        for (int x = 0; x < count; x++)
        {
            GameObject Temporary;
            Vector3 PinPosition = FirstPin.transform.position;
            Temporary =
            Instantiate(PartPrefab, new Vector3(PinPosition.x, PinPosition.y + PartDistance * (x + 1), PinPosition.z),
            Quaternion.identity, ParentObject.transform);
            Temporary.transform.eulerAngles = new Vector3(180, 0, 0);

            Temporary.name = ParentObject.transform.childCount.ToString();
            

            if (x == 0)
            { 
                //lina buduje siê od do³u
                 Destroy(Temporary.GetComponent<CharacterJoint>());

             
            }
           
            else
            {
            
                Temporary.GetComponent<CharacterJoint>().connectedBody =
                    ParentObject.transform.Find((ParentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
            if (snapLast ==true&&x==count-1)
            {
                SnapLast(Temporary);
                snapLast = false;
            }
        }
    }
   
    

    private void SnapLast(GameObject Temporary)
    {
        Temporary.transform.GetComponent<Rigidbody>().isKinematic = true;
    }
}
