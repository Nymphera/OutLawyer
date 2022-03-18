using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HelpLines : MonoBehaviour
{
    private int childCount,activeChildCount=0;
    [SerializeField]
    private Transform[] childs,activeChilds;
    [SerializeField]
    private Evidence[] evidences;
    [SerializeField]
    private Vector3[] points;

    private Line Line;
    [SerializeField]
    private Transform LineParent;
    [SerializeField]
    private GameObject linePrefab;
    private EventTrigger EventTrigger;
    private void Awake()
    {
        EventTrigger.OnEvidenceUnlocked += EventTrigger_OnEvidenceUnlocked;
        
    }
   
    private void EventTrigger_OnEvidenceUnlocked(Evidence evidence)
    {
        Debug.Log(evidence);
        for(int i = 0; i < childCount; i++)
        {
            if (childs[i].GetComponent<EvidenceDisplay>().Evidence == evidence)
            {
                childs[i].gameObject.SetActive(true);
            }
        }
       // GameObject.Find(evidence.name.ToString()).SetActive(true);
    }

    private void Start()
    {
        childCount = transform.childCount;
        childs = new Transform[childCount];
        for(int i = 0; i < childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
        for(int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                activeChildCount++;
            }
        }
        

        activeChilds = new Transform[activeChildCount];
        evidences = new Evidence[activeChildCount];
        points = new Vector3[activeChildCount];
        int index = 0;
        
        for(int  i= 0; i < childCount; i++)
        {
            
            if (transform.GetChild(i).gameObject.activeSelf==true)
            { 
                activeChilds[index] = transform.GetChild(i);
                points[index] = activeChilds[index].GetChild(1).position;
                evidences[index] = activeChilds[index].GetComponent<EvidenceDisplay>().Evidence;
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
                        Line.CreateLine();
                        Line.SetColor("White");
                    }
            }

        }

    }
}
