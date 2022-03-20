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
    [SerializeField]
    private List<Evidence.Conection> conections;

    private Line Line;
    [SerializeField]
    private Transform LineParent;
    [SerializeField]
    private GameObject linePrefab;
   
    private int elements=0;
    private void Awake()
    {
        GameManager.OnGameStateChanged += Create_HelpLines;
        EventTrigger.OnEvidenceUnlocked += EventTrigger_OnEvidenceUnlocked;
       
    }
    private void Start()
    {
        Create_HelpLines(GameState.Office);
        
     
  
       
    }
    public void Create_HelpLines(GameState state)
    {

       
        if (state == GameState.Office)
        {
           
            conections = new List<Evidence.Conection>();
            childCount = transform.childCount;
            childs = new Transform[childCount];

          
            for (int i = 0; i < childCount; i++)                // pêtla dodaj¹ca wszystkie dowody do tablicy
            {
                childs[i] = transform.GetChild(i);
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    activeChildCount++;
                }
            }



            activeChilds = new Transform[activeChildCount];
            evidences = new Evidence[activeChildCount];
            points = new Vector3[activeChildCount];
            
            int index = 0;

            for (int i = 0; i < childCount; i++)        //pêtla dodaj¹ca aktywne dowody,pozycje,zdjêcia do tablic
            {

                if (transform.GetChild(i).gameObject.activeSelf == true)
                {
                    activeChilds[index] = transform.GetChild(i);
                    points[index] = activeChilds[index].GetChild(1).position;
                    evidences[index] = activeChilds[index].GetComponent<EvidenceDisplay>().Evidence;
                    index++;
                }

            }
            for (int i = 0; i < activeChildCount; i++)      //pêtla z wszystkimi po³¹czeniami
            {
                int count = evidences[i].conection.Length;

                for (int j = 0; j < count; j++)
                {
                    
                    conections.Add(evidences[i].conection[j]);

                }
            }
            for (int i = 0; i < activeChildCount; i++)
            {
                int conectLength = evidences[i].conection.Length;


                for (int j = 0; j < conectLength; j++)
                {
                    for (int k = 0; k < activeChildCount; k++)
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
            Debug.Log(LineParent.childCount);
        }
        activeChildCount = 0;
    }
    private void EventTrigger_OnEvidenceUnlocked(Evidence evidence)
    {
        Debug.Log(evidence);
        for (int i = 0; i < childCount; i++)
        {
            if (childs[i].GetComponent<EvidenceDisplay>().Evidence == evidence)
            {
                childs[i].gameObject.SetActive(true);
            }
        }

    }
 
}
public class Lines
{
    public Transform FirstPoint,SecondPoint;
    public Evidence.Conection Conection;

}