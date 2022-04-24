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
    public void SetPoint(int index,Vector3 position)
    {
        lineRenderer.SetPosition(index, position);
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
        
            Vector3 startPosition = lineRenderer.GetPosition(0);
            Vector3 endPosition = lineRenderer.GetPosition(1);
            Vector3 pos = startPosition;
            while (pos != endPosition)
            {
                pos = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime)/animationDuration );
                lineRenderer.SetPosition(1, pos);
            yield return null;
            }
    }
    public void AddColliderToLine()
    {
        
       Vector3 start = lineRenderer.GetPosition(0);
       Vector3 end = lineRenderer.GetPosition(1);

        gameObject.tag= "ColliderLine";
        var startPos = start;
        var endPos = end;
        BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider>();
        //col.isTrigger = true;
        
        col.transform.parent =transform;

        col.tag = "ColliderLine";
        float lineLength = Vector3.Distance(startPos, endPos);
        col.size = new Vector3(lineLength, 0.1f, 0.04f);
        Vector3 midPoint = (startPos + endPos) / 2;
        col.transform.position = midPoint;
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(angle, 0, 90);
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
            this.conectionType = ConectionType.Yellow;
        }
        if (color == "Green")
        {
            lineRenderer.material = Green;
            this.conectionType = ConectionType.Green;
        }
        if (color == "Red")
        {
            lineRenderer.material = Red;
            this.conectionType = ConectionType.Red;
        }
        if (color == "Blue")
        {
            lineRenderer.material = Blue;
            this.conectionType = ConectionType.Blue;
        }
        if (color == "White")
        {
            lineRenderer.material = White;
            
        }
        if (color == "Black")
        {
            lineRenderer.material = White;
            this.conectionType = ConectionType.Black;
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

