using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "DialogOption", menuName = "Dialogs/DialogOption", order = 1)]
public class DialogOption : ScriptableObject
{
    

   
    public string text;     // w to pole trzeba wpisaæ tekst który bêdzie pojawiaæ siê w dymku
    public CrossPoint nextCrossPoint; // tu wybiermay  nastêpnego crossPointa np "A2"
    public CrossPoint earlierCrossPoint;
    public Strategy strategy;   // strategia jak¹ zawiera ten dialog
    [TextArea(3,10)]
    public string[] sentences;
    
}
