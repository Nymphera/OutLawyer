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
    GameObject[] predictedBars = new GameObject[5];
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
   
    public void GrowPredictedScore()
    {
        Result[] updatedResults;
        updatedResults=DialogManager.Instance.GetUpadatedResults(dialogOption.strategy);
        
        predictedBars=DialogManager.Instance.predictedBars;
        StartCoroutine(GrowResults(updatedResults));
    }
    public void FallPredictedScore()
    {
        Result[] updatedResults ;
        updatedResults = DialogManager.Instance.GetUpadatedResults(dialogOption.strategy);
        
        predictedBars = DialogManager.Instance.predictedBars;
        StopAllCoroutines();
        FallResults(updatedResults);
    }
    private IEnumerator GrowResults(Result[] updatedresults)
    {
        float startTime = Time.time;
        float firstBarValue = predictedBars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount;
        float secondBarValue = predictedBars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount;



        float newFirstValue = firstBarValue + barIncrease;
        float newSecondValue = secondBarValue + barIncrease;

        float distance = barIncrease;
        float value1, value2;
        while (distance > 0.01f)
        {
            value1 = Mathf.Lerp(firstBarValue, newFirstValue, (Time.time - startTime)*2);
            value2 = Mathf.Lerp(secondBarValue, newSecondValue, (Time.time - startTime)*2);

            distance = newFirstValue - value1;
            predictedBars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount = value1;
            predictedBars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount = value2;
            yield return null;
        }
        
        predictedBars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount = newFirstValue;
        predictedBars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount = newSecondValue;
    }
    private void FallResults(Result[] updatedresults)
    {
        predictedBars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount = DialogManager.Instance.bars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount;
        predictedBars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount = DialogManager.Instance.bars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount;
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
