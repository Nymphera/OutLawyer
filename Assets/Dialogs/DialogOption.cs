using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "DialogOption", menuName = "Dialogs/DialogOption", order = 1)]
public class DialogOption : ScriptableObject
{
    

   
        public string text;     // w to pole trzeba wpisa� tekst kt�ry b�dzie pojawia� si� w dymku
        public string nextCrossPointName; // tu wpisujemy nazw� nast�pnego crossPointa np "A2"
        public Strategy strategy;   // strategia jak� zawiera ten dialog
    

}