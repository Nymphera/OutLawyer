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
    public List<Evidence.Conection> conections;
    [SerializeField]
    private List<Line> lines;

    private Line Line;
    [SerializeField]
    private Transform LineParent;
    [SerializeField]
    private GameObject linePrefab;
    public HelpLines Instance;
    
    
    
    private void Awake()
    {
     Instance = this;
        GameManager.OnGameStateChanged += Create_HelpLines;
        EventTrigger.OnEvidenceUnlocked += EventTrigger_OnEvidenceUnlocked;
        PinBoardLogic.OnLineCreated += PinBoardLogic_OnLineCreated;
       
    }

    private void PinBoardLogic_OnLineCreated(Line.Conection conection)
    {
        Debug.Log(conection.FirstEvidence+" and "+conection.ConectedEvidence);
        for(int i = 0; i < conections.Count; i++)
        {
            
            if ((lines[i].conection.FirstEvidence == conection.FirstEvidence && lines[i].conection.ConectedEvidence == conection.ConectedEvidence) || (lines[i].conection.FirstEvidence == conection.ConectedEvidence && lines[i].conection.ConectedEvidence == conection.FirstEvidence))
            {
                lines[i].transform.GetComponent<LineRenderer>().enabled = false;
            }
        }
      
    }

    private void Start()
    {
        Create_HelpLines(GameState.Office);
        
     
  
       
    }
    public void Create_HelpLines(GameState state)
    {
       


        if (state == GameState.Office)
        {
            SetAllTables();

            int index=0;
            for (int i = 0; i < activeChildCount; i++)                 //ryswoanie linii JEŒLI
                for (int j = 0; j < conections.Count; j++)
                {
                    if (evidences[i] == conections[j].ConectedEvidence)     //jeœli dowód jest w po³¹czeniach innego dowodu
                    {

                        for (int k = 0; k < activeChildCount; k++)
                        {
                            if (evidences[k] == conections[j].FirstEvidence)    //
                                index = k;
                        }
                        Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
                        Line.firstEvidence = evidences[i];
                        Line.secondEvidence = evidences[index];
                        Line.AddPoint(points[i]);

                        Line.AddPoint(points[index]);
                       
                        Line.SetColor("White");
                        lines.Add(Line);
                    }
                }
            
        }
    }
        private void SetAllTables()
    {
        activeChildCount = 0;

        childCount = transform.childCount;


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
                conections.Add(evidences[i].conection[j]);
            }
        }
        for (int i = 0; i < conections.Count; i++)       //sortowanie i usuwanie powtarzaj¹cych siê wyrazów w tablicy po³¹czeñ
        {
            for (int j = 0; j < conections.Count; j++)
            {
                if (conections[i].conectNumber == conections[j].conectNumber && i != j)
                {
                    conections.RemoveAt(j);
                }
            }
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
