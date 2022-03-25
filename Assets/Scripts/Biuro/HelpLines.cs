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
    private void PinBoardLogic_OnLineDeleted(Line line)
    {
        for (int i = 0; i < lines.Count; i++)
        {

            if ((lines[i] == line))
            {
                lines[i].transform.GetComponent<LineRenderer>().enabled = true;
            }
        }
        LineCounter();
    }

    private void PinBoardLogic_OnLineCreated(Line line)
    {
        
        for(int i = 0; i < lines.Count; i++)
        {
            
            if ((lines[i].firstEvidence==line.firstEvidence&&lines[i].secondEvidence==line.secondEvidence)||(lines[i].firstEvidence == line.secondEvidence && lines[i].secondEvidence == line.firstEvidence))
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
        
        for (int i=0;i<lines.Count;i++)
        {
            if (lines[i].transform.GetComponent<LineRenderer>().enabled)
            {
                if (lines[i].conectionType == ConectionType.Red)
                {
                    redCount++;
                }
                if (lines[i].conectionType == ConectionType.Yellow)
                {
                    yellowCount++;
                }
                if (lines[i].conectionType == ConectionType.Blue)
                {
                    blueCount++;
                }
                if (lines[i].conectionType == ConectionType.Green)
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

            for (int i = 0; i < activeChildCount; i++)      //tablica dowodów
            {

                for (int j = 0; j < activeChildCount; j++)      // tablica po³¹czeñ
                {
                    int conectionsCount = evidences[j].Conections.Length;
                    if (i < j)
                    {
                        for (int k = 0; k < conectionsCount; k++)
                        {
                            if (evidences[i] == evidences[j].Conections[k].conected)
                            {
                                Line = Instantiate(linePrefab, LineParent).GetComponent<Line>();
                                Line.firstEvidence = evidences[i];
                                Line.secondEvidence = evidences[j];
                                Line.conectionType = evidences[j].Conections[k].ConectionType;
                                Line.SetColor("White");
                                Line.AddPoint(points[i]);
                                Line.AddPoint(points[j]);
                                lines.Add(Line);
                                
                            }
                        }
                    }
                }
            }






            









         /*   for (int i = 0; i < activeChildCount; i++)                 //ryswoanie linii JEŒLI
                for (int j = 0; j < conections.Count; j++)
                {
                    if (evidences[i] == conections[j].ConectedEvidence)     //jeœli dowód jest w po³¹czeniach innego dowodu
                    {

                        for (int k = 0; k < activeChildCount; k++)
                        {
                            if (evidences[k] == conections[j].FirstEvidence)    //
                                index = k;
                        }
                        
                        
                        
                        Line.conection.conectNumber = conections[j].conectNumber;
                        Line.AddPoint(points[i]);

                        Line.AddPoint(points[index]);
                       
                        Line.SetColor("White");     // dodaje siê jednoczeœnie line.conection.color
                        lines.Add(Line);
                    }
                }
            lines.Sort((a, b) => { return a.conection.conectNumber.CompareTo(b.conection.conectNumber); });
         */
        }
    }
        private void SetAllTables()
    {
        activeChildCount = 0;

        childCount = transform.childCount;


        childs = new Transform[childCount];
        



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


        


        /*for (int i = 0; i < conections.Count; i++)       //sortowanie i usuwanie powtarzaj¹cych siê wyrazów w tablicy po³¹czeñ
        {
            for (int j = 0; j < conections.Count; j++)
            {
                if (conections[i].conectNumber == conections[j].conectNumber && i != j)
                {
                    conections.RemoveAt(j);
                }
            }
        }*/
        //sortowanie dowodów
        //conections.Sort((a, b) => { return a.conectNumber.CompareTo(b.conectNumber); });
       
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
