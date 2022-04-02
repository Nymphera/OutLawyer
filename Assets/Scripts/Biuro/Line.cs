using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    public List<Vector3> points = new List<Vector3>();
    [HideInInspector] public int pointsCount = 0;
    public Evidence firstEvidence;
    public Evidence secondEvidence;
    public ConectionType conectionType;
    public bool isConectionGood=false;

    //public Conection conection;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public float animationDuration; 
    [SerializeField]
    private Material Yellow, Green, Red, Blue, White;
    
    public void AddPoint(Vector3 vector)
    {
        
        
        points.Add(vector);
        pointsCount++;
        lineRenderer.positionCount++;
        
        lineRenderer.SetPosition(pointsCount - 1, vector);
      
        
    }
    public void Render()
    {
        lineRenderer.SetPosition(0,points[0]);
        lineRenderer.SetPosition(1,points[1]);
    }
    public IEnumerator AnimateLine()
    {
       // lineRenderer.SetPosition(0, points[0]);
        float startTime = Time.time;
            Vector3 startPosition = points[0];
            Vector3 endPosition = points[1];
            Vector3 pos = startPosition;
            while (pos != endPosition)
            {
                pos = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime)/animationDuration );
                lineRenderer.SetPosition(1, pos);
            yield return null;
            }
    }
    public void SetWidth(float value)
    {
        lineRenderer.startWidth = value;
        lineRenderer.endWidth = value;
    }
   
    public void SetColor(string color)
    {

        if (color == "Yellow")
        {
            lineRenderer.material = Yellow;
           // conection.conectionColor = ConectionType.Yellow;
        }
        if (color == "Green")
        {
            lineRenderer.material = Green;
          //  conection.conectionColor = ConectionType.Green;
        }
        if (color == "Red")
        {
            lineRenderer.material = Red;
          //  conection.conectionColor = ConectionType.Red;
        }
        if (color == "Blue")
        {
            lineRenderer.material = Blue;
            //conection.conectionColor = ConectionType.Blue;
        }
        if (color == "White")
        {
            lineRenderer.material = White;
            //conection.conectionColor = ConectionType.White;
        }
        if (color == "Black")
        {
            lineRenderer.material = White;
           // conection.conectionColor = ConectionType.Black;
        }
    }
    [Serializable]
    public class Conection
    {
        public Evidence FirstEvidence;
        public Evidence ConectedEvidence;
        
        public ConectionType conectionColor;
        public string Conclusion;
        
        public int conectNumber;

    }
}
[Serializable]
public enum ConectionType
{
    Yellow, // Motyw
    Red,    //Sprzecznoœæ
    Blue,   //Relacja
    Green,   //Dowód
    White,
    Black
}

