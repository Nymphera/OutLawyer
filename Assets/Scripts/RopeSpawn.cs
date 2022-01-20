using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject PartPrefab, ParentObject;
    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;
    [SerializeField]
    [Range(1, 1000)]
    int Length = 1;
    [SerializeField]
    private float PartDistance=0.21f;
    
    

    private void Update()
    {
        if (reset == true)
        {
            DestroyRope();
            reset = false;
        }
        if (spawn == true)
        {
            SpawnRope();
            spawn = false;
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
        int count = (int)(Length / PartDistance);
        Debug.Log(count);
        for(int x = 0; x < count; x++)
        {
            GameObject Temporary;
            Temporary = Instantiate(PartPrefab, new Vector3(transform.position.x, transform.position.y+PartDistance*(x+1), transform.position.z),Quaternion.identity, ParentObject.transform);
            Temporary.transform.eulerAngles = new Vector3(180, 0, 0);
            
            Temporary.name = ParentObject.transform.childCount.ToString();

            if (x == 0)
            {
                //ParentObject.transform.Find((ParentObject.transform.childCount - 1).ToString()).GetComponent<Transform>();

                 Destroy(Temporary.GetComponent<CharacterJoint>());
            }
            else
            {
                Temporary.GetComponent<CharacterJoint>().connectedBody = 
                    ParentObject.transform.Find((ParentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}
