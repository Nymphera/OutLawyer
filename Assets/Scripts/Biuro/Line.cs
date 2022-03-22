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


    public float animationDuration; 
    [SerializeField]
    private Material Yellow, Green, Red, Blue, White;
    
    public void AddPoint(Vector3 vector)
    {
        pointsCount++;
        points.Add(vector);
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, vector);
    }
    public void CreateLine()
    { 
                
      //  lineRenderer.SetPosition(0, points[0]);
      //  lineRenderer.SetPosition(1, points[1]);
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
        }
        if (color == "Green")
        {
            lineRenderer.material = Green;
        }
        if (color == "Red")
        {
            lineRenderer.material = Red;
        }
        if (color == "Blue")
        {
            lineRenderer.material = Blue;
        }
        if (color == "White")
        {
            lineRenderer.material = White;
        }
    }

}
        
