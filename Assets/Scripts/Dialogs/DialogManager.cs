using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    int currentLevel=0;

    private Image lawyerIcon;
    WaitForSeconds wait = new WaitForSeconds(1f);
    Transform tree;
    [SerializeField]
    private float animationDuration=10;
    
    GameObject Lawyer, HandshakeResult,EvilLawyerResult,MaskResult,CrabResult,BurningChairResult;

    [SerializeField]
    private float barIncrease = 0.5f;
    [SerializeField]
    private Result result1, result2, result3, result4, result5;
    private Result[] results;
    private void Awake()
    {
        
        GetResults();
        ClearResultsBars();
        

        DialogOptionDisplay.OnDialogButtonClicked += DialogOptionDisplay_OnDialogButtonClicked;
        
        
    }

    

    private void OnDestroy()
    {
        DialogOptionDisplay.OnDialogButtonClicked -= DialogOptionDisplay_OnDialogButtonClicked;
    }
    private void Start()
    {
        Lawyer = GameObject.Find("LawyerImage(Clone)");
        lawyerIcon = Lawyer.GetComponent<Image>();
        tree = Lawyer.transform.parent;
    }

    private void DialogOptionDisplay_OnDialogButtonClicked(DialogOption dialogOption, Vector3 buttonPosition)
    {
       

        int length=dialogOption.earlierCrossPoint.ConectedDialogOptions.Length;
        
       for (int i = 0; i < length; i++)
        {
            if (Lawyer.GetComponent<DialogLawyer>().currentCrossPoint == dialogOption.earlierCrossPoint)
            {
                StartCoroutine(MoveLawyer(dialogOption, buttonPosition));

                StartCoroutine(UpdateScore(dialogOption.strategy));
                Lawyer.GetComponent<DialogLawyer>().currentCrossPoint = dialogOption.nextCrossPoint;

                
                
                
            }
        }

       
        
    }
    /// <summary>
    /// Ustawia wartoœæ <b>ResultBar</b> na 0.
    /// </summary>
    private void ClearResultsBars()
    {

        foreach(Result result in results)
        {
            result.ResultBar.gameObject.GetComponent<Image>().fillAmount = 0.1f;
        }
        
    }
    /// <summary>
    /// <b>GetResults</b>
    /// Przypisuje zmiennym result1,result2,...wartoœci <code>Result</code>
    /// </summary>
    private void GetResults()
    {
        Transform resultsTrans = GameObject.Find("Results").transform;
        
        result1 = resultsTrans.GetChild(0).gameObject.GetComponent<Result>();
        result2 = resultsTrans.GetChild(1).gameObject.GetComponent<Result>();
        result3 = resultsTrans.GetChild(2).gameObject.GetComponent<Result>();
        result4 = resultsTrans.GetChild(3).gameObject.GetComponent<Result>();
        result5 = resultsTrans.GetChild(4).gameObject.GetComponent<Result>();
        results = new Result[] { result1, result2, result3, result4, result5 };
    }
    /// <summary>
    /// Tu mo¿na zniszczyæ odpowiednie linie dialogów, mo¿na te¿ zniszczyæ opcje których ju¿ nie mo¿emy wybraæ gdyby ktoœ chcia³.
    /// </summary>
    private void DestroyLowerLevel()
    {
        currentLevel++;
       // Debug.Log("Current Level: " + currentLevel);
        Destroy(GameObject.Find("Level " + (currentLevel - 1)));
    }

    private IEnumerator UpdateScore(Strategy strategy) 
    {
        switch (strategy)
  
        {
            case Strategy.LuŸnaGadka:
                break;
            case Strategy.Podstêp:
                break;
            case Strategy.Profesjonalizm:
                break;
            case Strategy.UrokOsobisty:
                break;
            case Strategy.ZimnaKrew:
                break;

            default:
                break;
        }
        foreach (Result result in results)
        {
            
            if (result.strategy1 == strategy || result.strategy2 == strategy)
            {
                float startTime = Time.time;

                float startvalue= result.ResultBar.GetComponent<Image>().fillAmount;
                float newValue = startvalue+barIncrease;
                Debug.Log(startvalue + " " + newValue);
                float distance = newValue-startvalue;
                float value;
                while (distance>0.01f)
                {
                    value = Mathf.Lerp(startvalue, newValue, (Time.time - startTime));
                    
                    distance = newValue - value;
                    result.ResultBar.GetComponent<Image>().fillAmount = value;
                    yield return null;
                }
                result.ResultBar.GetComponent<Image>().fillAmount = newValue;
            }
        }
    }

    private IEnumerator MoveLawyer(DialogOption dialogOption, Vector3 buttonPosition)
    {
        

        Vector3 nextCrossPointPosition = GameObject.Find(dialogOption.nextCrossPoint.name).GetComponent<RectTransform>().localPosition;
        Vector3 newTreePosition = new Vector3(tree.localPosition.x, tree.localPosition.y - 600, tree.localPosition.z);
        float distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, buttonPosition);
        float startTime = Time.time;
   
        while (distanceToTarget > 0.1f)     //move to dialog
        {
            
            distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, buttonPosition);

            lawyerIcon.rectTransform.localPosition = Vector3.Lerp(lawyerIcon.rectTransform.localPosition, buttonPosition,(Time.time-startTime)/animationDuration);

            yield return null;
        }
        lawyerIcon.rectTransform.localPosition = buttonPosition;

        //Debug.Log("Wait for dialog");
        yield return wait;  //wait Until


        startTime = Time.time;
        distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition);    //przesuwanie do crosspointa
        while (distanceToTarget > 0.1f)
        {
            distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition);
            lawyerIcon.rectTransform.localPosition = Vector3.Lerp(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition, (Time.time - startTime) / animationDuration);
            yield return null;
        }
        lawyerIcon.rectTransform.localPosition = nextCrossPointPosition;
        yield return null;
        DestroyLowerLevel();

        startTime = Time.time;  
        distanceToTarget = Vector3.Distance(tree.localPosition, newTreePosition);       //przesuwanie drzewka
        while (distanceToTarget > 0.1f)
        {            
            distanceToTarget = Vector3.Distance(tree.localPosition, newTreePosition);
            tree.localPosition = Vector3.Lerp(tree.localPosition, newTreePosition, (Time.time - startTime) / animationDuration);
            yield return null;
        }

        tree.localPosition = newTreePosition;
       
        
    }
}
