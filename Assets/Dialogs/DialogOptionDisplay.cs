using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DialogOptionDisplay : MonoBehaviour
{
    public DialogOption dialogOption;
    public Vector3 buttonPosition;
    
    [SerializeField]
    private Sprite strategy1, strategy2, strategy3, strategy4, strategy5;
    public static event Action<DialogOption, Vector3> OnDialogButtonClicked;
    private GameObject dialogText;
    private float barIncrease = 0.2f;
    private void Awake()
    {
       
    }


    public void ShowButtonText()
    {
        if (DialogManager.Instance.currentState == DialogState.playerTurn)
        {
            dialogText = GameObject.Find("DialogText");
            dialogText.GetComponent<Text>().text = GetComponent<DialogOptionDisplay>().dialogOption.text;
        }
    }
       
    public void HideButtonText()
    {
        if (DialogManager.Instance.currentState == DialogState.playerTurn)
        {
            dialogText = GameObject.Find("DialogText");
            dialogText.GetComponent<Text>().text = "";
        }
        
    }

    private void Update()
    {
        GetComponent<Button>().enabled = DialogManager.Instance.currentState == DialogState.playerTurn;        
    }

    public void Click()
    {
        OnDialogButtonClicked(dialogOption,buttonPosition);
    }
   
    public void UpdatePredictedScore()
    {
        Result[] updatedResults = new Result[2];
        updatedResults=DialogManager.Instance.GetUpadatedResults(dialogOption.strategy);

    }
    public void RenderImage()
    {
        
        
        if (dialogOption.strategy == Strategy.ZimnaKrew)
        {
            GetComponent<Image>().sprite = strategy1;
        }
        else
                    if (dialogOption.strategy == Strategy.Podstêp)
        {
            GetComponent<Image>().sprite = strategy2;
        }
        else
                    if (dialogOption.strategy == Strategy.Profesjonalizm)
        {
            GetComponent<Image>().sprite = strategy3;
        }
        else
                    if (dialogOption.strategy == Strategy.LuŸnaGadka)
        {
            GetComponent<Image>().sprite = strategy4;
        }
        else
                    if (dialogOption.strategy == Strategy.UrokOsobisty)
        {
            GetComponent<Image>().sprite = strategy5;
        }
    }
}
