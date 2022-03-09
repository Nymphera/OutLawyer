using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private LineRenderer LineRenderer;
    [SerializeField] public List<Vector3> points = new List<Vector3>();
    [SerializeField] public int pointsCount = 0;
    [SerializeField] EdgeCollider2D edgeCollider;

    public void AddPoint(Vector3 newPoint)
    {
        points.Add(newPoint);
        pointsCount++;
        LineRenderer.positionCount = pointsCount;
        LineRenderer.SetPosition(pointsCount - 1, newPoint);
        Debug.Log("newPoint: "+newPoint);
        //if (pointsCount > 1)
          //  edgeCollider.points = points.ToArray();
    }
    public void SetColor(Gradient colorGradient)
    {
        LineRenderer.colorGradient = colorGradient;
    }
    public void ShowPoints()
    {foreach(Vector3 V in points)
        {
            Debug.Log(V);
        }
    }
    public void SetLineWidth(float width)
    {
        LineRenderer.startWidth = width;
        LineRenderer.endWidth = width;     
    }
}
