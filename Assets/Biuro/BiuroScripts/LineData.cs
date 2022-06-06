using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineData 
{
    public Evidence firstEvidence;
    public Evidence secondEvidence;
    public ConectionType conectionType;
    public bool isConectionGood = false;
    public bool wasLineBurned = false;
}
