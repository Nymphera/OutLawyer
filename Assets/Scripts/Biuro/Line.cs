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
    public int pointsCount = 0;
    public Evidence firstEvidence;
    public Evidence secondEvidence;
    public Conection conection;

    public float animationDuration; 
    [SerializeField]
    private Material Yellow, Green, Red, Blue, White;
    
    public void AddPoint(Vector3 vector)
    {
        SetConection();
        pointsCount++;
        points.Add(vector);
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, vector);
    }

    private void SetConection()
    {
        conection.FirstEvidence = firstEvidence;
        conection.ConectedEvidence = secondEvidence;
        
    }

    public void AnimateLine()
    {
        float startTime = Time.time;
        
        Vector3 startPosition = points[0];
        Vector3 endPosition = points[1];
        lineRenderer.SetPosition(0, startPosition);
        Vector3 pos = startPosition;
        while (pos != endPosition)
        {
            pos = Vector3.Lerp(startPosition, endPosition,(Time.time-startTime)/animationDuration);
            lineRenderer.SetPosition(1, pos);

        }
        lineRenderer.SetPosition(1, endPosition);
    }
    
   
    public void SetColor(string color)
    {

        if (color == "Yellow")
        {
            lineRenderer.material = Yellow;
            conection.conectionColor = ConectionType.Yellow;
        }
        if (color == "Green")
        {
            lineRenderer.material = Green;
            conection.conectionColor = ConectionType.Green;
        }
        if (color == "Red")
        {
            lineRenderer.material = Red;
            conection.conectionColor = ConectionType.Red;
        }
        if (color == "Blue")
        {
            lineRenderer.material = Blue;
            conection.conectionColor = ConectionType.Blue;
        }
        if (color == "White")
        {
            lineRenderer.material = White;
            conection.conectionColor = ConectionType.White;
        }
        if (color == "Black")
        {
            lineRenderer.material = White;
            conection.conectionColor = ConectionType.Black;
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
public enum ConectionType
{
    Yellow, // Motyw
    Red,    //Sprzecznoœæ
    Blue,   //Relacja
    Green,   //Dowód
    White,
    Black
}

