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
    [SerializeField]
    Material Yellow, Green, Blue, Red;

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
        Transform Temporary;
        Ray Ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(Ray, out Hit, 100))
        {
            if (Hit.transform.gameObject.layer == 7)
            {
               
                Temporary = Hit.transform.GetChild(0);
                print(Temporary.name);
              
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
        
        float Length=Vector3.Distance(FirstPin.transform.position,SecondPin.transform.position);
        int count = (int)(Length / PartDistance);
        Debug.Log(count);

        


        for (int x = 0; x < count; x++)
        {
            Vector3 SpawnDirection = -(FirstPin.transform.position - SecondPin.transform.position);
            GameObject Temporary;
            Vector3 PinPosition = FirstPin.transform.position;
            PartPrefab.transform.GetComponent<MeshRenderer>().material =LineColor();
            Temporary =
            Instantiate(PartPrefab, new Vector3(PinPosition.x +PartDistance*(x*SpawnDirection.x), 
            PinPosition.y + PartDistance * (x*SpawnDirection.y), PinPosition.z -0.1f ),
            new Quaternion(SpawnDirection.x,SpawnDirection.y,SpawnDirection.z,0), ParentObject.transform);
            Temporary.transform.eulerAngles = new Vector3(180, 0, 0);

            Temporary.name = ParentObject.transform.childCount.ToString();
            

            if (x == 0)
            {
                //lina buduje siê od do³u
                // Destroy(Temporary.GetComponent<CharacterJoint>());
                Temporary.GetComponent<CharacterJoint>().connectedBody = FirstPin.GetComponent<Rigidbody>();   


            }
           
            else if(x==count-1)
            {
                //to prawie dzia³a, tylko bierze pozycje pivot evidence, zamiast pozycji pina
                Temporary.GetComponent<CharacterJoint>().connectedBody = 
                ParentObject.transform.Find((ParentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
                CharacterJoint LastJoint= Temporary.AddComponent<CharacterJoint>();

                
                LastJoint.connectedBody=SecondPin.GetComponent<Rigidbody>();
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
   
    
    private Material LineColor()
    {
        return Yellow;
    }
    private void SnapLast(GameObject Temporary)
    {
        Temporary.transform.GetComponent<Rigidbody>().isKinematic = true;
    }
}
