using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CrossPoint", menuName = "Dialogs/CrossPoint", order = 2)]
public class CrossPoint : ScriptableObject
{
    //co� �eby da�o si� rozpozna� r�nic� mi�dzy tymi crosspointami typu name/ int
    public string crossPointName;
    public DialogOption[] ConectedDialogOptions; //to nie jest najlepszy pomys� �eby to by�o public
}