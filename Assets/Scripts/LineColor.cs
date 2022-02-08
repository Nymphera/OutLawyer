using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColor : MonoBehaviour
{
   
    private Material LineMaterial;

    public GameObject LinePrefab;
    private void Start()
    {
        LineMaterial = LinePrefab.GetComponent<Material>();
       Debug.Log(LinePrefab.transform.GetComponent<Material>().name);
    }


    public void SetLineMaterial()
    {
        Debug.Log(LinePrefab.transform.GetComponent<Material>().name);
        LineMaterial.SetColor(0, Color.yellow);
    }
   private enum Colors
    {
        Green,Red,Blue,Yellow
    }

}
