using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewResult", menuName = "Dialogs/Results", order = 0)]
[System.Serializable]
public class Result : ScriptableObject
{
    public int resultNumber;
    public Strategy strategy1, strategy2;
    
    public Color ResultBarColor;
    public string ResultText;
    public Sprite resultImage;

    public string[] sentences;

    public Evidence evidenceToUnlock;
}
