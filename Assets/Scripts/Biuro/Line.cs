using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
 public List<Vector3> points = new List<Vector3>();
 public int pointsCount = 0;
   
 
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private Material Yellow, Green, Red, Blue,White;
    public void AddPoint(Vector3 newPoint)
    {
        points.Add(newPoint);
        pointsCount++;
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
        
      
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
    public void ShowPoints()
    { foreach (Vector3 V in points)
        {
            Debug.Log(V);
        }
    }
    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
    public IEnumerator AnimateLine()
    {
       
        float segmentDuration = animationDuration / pointsCount;

        for (int i = 0; i < pointsCount ; i++)
        {
            float startTime = Time.time;

            Vector3 startPosition = points[i];
            Vector3 endPosition = points[i + 1];

            Vector3 pos = startPosition;
            while (pos != endPosition)
            {
                float t = (Time.time - startTime) / segmentDuration;
                pos = Vector3.Lerp(startPosition, endPosition, t);

                // animate all other points except point at index i
                for (int j = i + 1; j < pointsCount; j++)
                    lineRenderer.SetPosition(j, pos);

                yield return null;
            }
        }
    } 
}
        
