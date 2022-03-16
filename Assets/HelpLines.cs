using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HelpLines : MonoBehaviour
{
    private int childCount,activeChildCount=0;
    [SerializeField]
    private Transform[] childs;
    [SerializeField]
    private Evidence[] evidences;
    [SerializeField]
    private Vector3[] points;

    private Line Line;
    [SerializeField]
    private Transform LineParent;
    [SerializeField]
    private GameObject linePrefab;
    private void Start()
    {
        childCount = transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                activeChildCount++;
            }
        }
        Debug.Log("active: " + activeChildCount);

        childs = new Transform[activeChildCount];
        evidences = new Evidence[activeChildCount];
        points = new Vector3[activeChildCount];
        int index = 0;
        
        for(int  i= 0; i < childCount; i++)
        {
            
            if (transform.GetChild(i).gameObject.activeSelf==true)
            {
                childs[index] = transform.GetChild(i);
                points[index] = childs[index].GetChild(1).position;
                evidences[index] = childs[index].GetComponent<EvidenceDisplay>().Evidence;
                index++;    
            }
           
        }
        for (int i = 0; i < activeChildCount; i++)
        {
            int conectLength=evidences[i].conection.Length;
            //foreach( Evidence.Conection conection in evidences[i].conection)
          
            for (int j = 0; j < conectLength; j++)
            {
                for(int k=0;k<activeChildCount;k++)
                if (evidences[k] == evidences[i].conection[j].ConectedEvidence)
                {
                        
                        
                        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
                        
                        Line.AddPoint(points[i]);
                        Line.AddPoint(points[k]);
                        Line.SetColor("White");
                    }
            }

        }

    }
}
