using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTreeCreator : MonoBehaviour
{   
    
    [SerializeField]
    private Dialog dialog;
    [SerializeField]
    private GameObject crossPointPrefab,dialogOptionPrefab,treeParent,linePrefab;
    float treeHeight;
    float treeWidth;

    private void Awake()
    {
        CreateTree();
    }
    public void CreateTree()
    {
        GameObject A=Instantiate(crossPointPrefab, new Vector3(0, 5, 0), Quaternion.identity,treeParent.transform);
        GameObject B = Instantiate(dialogOptionPrefab,new Vector3 (0,-5,0),Quaternion.EulerRotation(0,0,45),treeParent.transform);
        Line line= Instantiate(linePrefab,treeParent.transform).GetComponent<Line>();
        line.SetWidth(0.5f);
        line.AddPoint(A.transform.position);
        line.AddPoint(B.transform.position);
        
    }
}
