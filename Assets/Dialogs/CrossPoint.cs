using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CrossPoint", menuName = "Dialogs/CrossPoint", order = 2)]
public class CrossPoint : ScriptableObject
{
    //coœ ¿eby da³o siê rozpoznaæ ró¿nicê miêdzy tymi crosspointami typu name/ int
    public string crossPointName;
    public DialogOption[] ConectedDialogOptions; //to nie jest najlepszy pomys³ ¿eby to by³o public
}