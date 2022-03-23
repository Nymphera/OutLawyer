using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HelpLines : MonoBehaviour
{
    private GameObject RedButton, GreenButton, YellowButton, BlueButton;
    private Text redText, greenText, yellowText, blueText;
    private int redCount=0, greenCount=0, yellowCount=0, blueCount=0;

    private int childCount,activeChildCount;
    [SerializeField]
    private Transform[] childs,activeChilds;
    [SerializeField]
    private Evidence[] evidences;
    [SerializeField]
    private Vector3[] points;
    [SerializeField]
    public List<Line.Conection> conections;
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
        PinBoardLogic.OnLineDeleted += PinBoardLogic_OnLineDeleted;

        RedButton = GameObject.Find("RedButton");
        GreenButton = GameObject.Find("GreenButton");
        BlueButton = GameObject.Find("BlueButton");
        YellowButton = GameObject.Find("YellowButton");

        redText = RedButton.transform.GetChild(1).GetComponent<Text>();
        greenText = GreenButton.transform.GetChild(1).GetComponent<Text>();
        blueText = BlueButton.transform.GetChild(1).GetComponent<Text>();
        yellowText = YellowButton.transform.GetChild(1).GetComponent<Text>();
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= Create_HelpLines;
        EventTrigger.OnEvidenceUnlocked -= EventTrigger_OnEvidenceUnlocked;
        PinBoardLogic.OnLineCreated -= PinBoardLogic_OnLineCreated;
        PinBoardLogic.OnLineDeleted -= PinBoardLogic_OnLineDeleted;
    }
    private void PinBoardLogic_OnLineDeleted(Line.Conection conection)
    {
        for (int i = 0; i < conections.Count; i++)
        {

            if ((lines[i].conection.FirstEvidence == conection.FirstEvidence && lines[i].conection.ConectedEvidence == conection.ConectedEvidence) || (lines[i].conection.FirstEvidence == conection.ConectedEvidence && lines[i].conection.ConectedEvidence == conection.FirstEvidence))
            {
                lines[i].transform.GetComponent<LineRenderer>().enabled = true;
            }
        }
        LineCounter();
    }

    private void PinBoardLogic_OnLineCreated(Line.Conection conection)
    {
        
        for(int i = 0; i < conections.Count; i++)
        {
            
            if ((lines[i].conection.FirstEvidence == conection.FirstEvidence && lines[i].conection.ConectedEvidence == conection.ConectedEvidence) || (lines[i].conection.FirstEvidence == conection.ConectedEvidence && lines[i].conection.ConectedEvidence == conection.FirstEvidence))
            {
                lines[i].transform.GetComponent<LineRenderer>().enabled = false;
            }
        }
        LineCounter();
    }

    private void Start()
    {
        Create_HelpLines(GameState.Office);

        LineCounter();      
  
       
    }

    private void LineCounter()
    {
        redCount = 0;
        greenCount = 0;
        yellowCount = 0;
        blueCount = 0;
        
        for (int i=0;i<conections.Count;i++)
        {
            if (lines[i].transform.GetComponent<LineRenderer>().enabled)
            {
                if (conections[i].conectionColor == ConectionType.Red)
                {
                    redCount++;
                }
                if (conections[i].conectionColor == ConectionType.Yellow)
                {
                    yellowCount++;
                }
                if (conections[i].conectionColor == ConectionType.Blue)
                {
                    blueCount++;
                }
                if (conections[i].conectionColor == ConectionType.Green)
                {
                    greenCount++;
                }
            }

            redText.text = redCount.ToString();
            greenText.text = greenCount.ToString();
            blueText.text = blueCount.ToString();
            yellowText.text = yellowCount.ToString();
        }
        
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
                       
                        Line.SetColor("White");     // dodaje siê jednoczeœnie line.conection.color
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
        conections = new List<Line.Conection>();



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
        //sortowanie dowodów
        //conections.Sort();
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
