using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject PartPrefab, ParentObject,Pin;
    [SerializeField]
    bool reset, spawn, snapLast;
    [SerializeField]
    [Range(1, 100)]
    int Length = 1;
    [SerializeField]
    private float PartDistance=0.21f;
    [SerializeField]
    Vector3 FirstPin, SecondPin;

    private Vector3 MousePos;
    


    private void Update()
    {

        Debug.Log(MousePosition());
        if (reset == true)
        {
            DestroyRope();
            reset = false;
        }
        if (spawn == true)
        {
            spawn = true;
            SpawnRope();
            spawn = false;
           
        }
        if (Input.GetMouseButton(1) )
        { //if (FirstPin == Vector3.zero&& SecondPin == Vector3.zero)
            FirstPin = MousePosition();
        
        }
        if (Input.GetMouseButton(0))
        {
            SecondPin = MousePosition();
        }
    }
   public Vector3 MousePosition()
    {
        GameObject Temporary;
        Ray Ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(Ray, out Hit, 100))
        {
            if (Hit.transform.gameObject.layer == 7)
            {
                Debug.Log("Pin!");
                Temporary = Hit.transform.gameObject;
                return Temporary.GetComponentInChildren<Transform>().position;
                
            }

            return Vector3.zero;

        }
        else
        {
            Debug.Log("no Pin :(");
            return Vector3.zero;
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
       Vector3 MousePos= MousePosition();
        int count = (int)(Length / PartDistance);
        Debug.Log(count);




        for (int x = 0; x < count; x++)
        {
            GameObject Temporary;

            Temporary =
            Instantiate(PartPrefab, new Vector3(MousePos.x, MousePos.y + PartDistance * (x + 1), MousePos.z),
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
