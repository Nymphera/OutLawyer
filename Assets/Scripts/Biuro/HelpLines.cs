using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HelpLines : MonoBehaviour
{
    private int childCount,activeChildCount;
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
    Evidence temporaryEvidence;
    int countConection;
    int index2;
    private bool isInTable;
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
        activeChildCount = 0;


        if (state == GameState.Office)
        {
           
            
            childCount = transform.childCount;

            Debug.Log("child Count: " + childCount);
          
           
            childs = new Transform[childCount];
            conections = new List<Evidence.Conection>();
          


            for (int i = 0; i < childCount; i++)                // pêtla dodaj¹ca wszystkie dowody do tablicy ACTIVE
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


            for (int i = 0; i < activeChildCount; i++)      //tablica dowodów
            {
                int conectLength = evidences[i].conection.Length;


                for (int j = 0; j < conectLength; j++)      // tablica po³¹czeñ
                {   
                    conections.Add( evidences[i].conection[j]);
                }
            }
            for(int i = 0; i < conections.Count; i++)       //sortowanie i usuwanie powtarzaj¹cych siê wyrazów w tablicy po³¹czeñ
            {
                for (int j = 0; j < conections.Count; j++)
                {
                    if (conections[i].conectNumber == conections[j].conectNumber && i != j)
                    {
                        conections.RemoveAt(j);
                    }
                }
            }
           
            for (int i = 0; i < activeChildCount; i++)      //zwraca pierwszy dowód z po³¹czenia
            {
                for (int j = 0; j < conections.Count; j++)
                {
                    if(evidences[i]== conections[j].ConectedEvidence)
                    {
                        temporaryEvidence = conections[j].FirstEvidence;
                    }
                }
            }
            for(int i = 0; i < activeChildCount; i++)
            {
                if (temporaryEvidence == evidences[i])
                {
                    index2 = i;
                }
            }

                for (int i=0;i<activeChildCount;i++)                 //ryswoanie linii JEŒLI
                    for (int j = 0; j < conections.Count;j++)
                    {
                    if (evidences[i]==conections[j].ConectedEvidence) 
                        {

                        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
                        //mam wzi¹æ conections.FirstEvidence i znaleŸæ jego indeks w tablicy dowodów
                        Line.AddPoint(points[i]);

                        //dodaj punkt conections[j].firstEvidence here
                        Line.AddPoint(points[index2]);
                        Line.CreateLine();
                        Line.SetColor("White");
                        }
                    }
            Debug.Log(LineParent.childCount);
        }

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
