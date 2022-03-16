using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HelpLines : MonoBehaviour
{
    private int childcount;
    private Transform[] childs;
    private Evidence[] evidences;
    private Vector3[] points;

    private Line Line;
    [SerializeField]
    private Transform LineParent;
    [SerializeField]
    private GameObject linePrefab;
    private void Start()
    {
        childcount = transform.childCount;
        childs = new Transform[childcount];
        evidences = new Evidence[childcount];
        points = new Vector3[childcount];
        points = new Vector3[childcount];
        
        for(int i = 0; i < childcount; i++)
        {
            childs[i]=transform.GetChild(i);
            points[i] = childs[i].GetChild(1).position;
            evidences[i] = childs[i].GetComponent<EvidenceDisplay>().Evidence;
        }
        for (int i = 0; i < childcount; i++)
        {
            int conectLength=evidences[i].conection.Length;

            for (int j = 0; j < conectLength; j++)
            {
                for(int k=0;k<childcount;k++)
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
