using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    

    private Image lawyerIcon;
    WaitForSeconds wait = new WaitForSeconds(1f);
    Transform tree;
    [SerializeField]
    private float animationDuration=10;
    [SerializeField]
    GameObject Lawyer;
    private void Awake()
    {
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

                Lawyer.GetComponent<DialogLawyer>().currentCrossPoint = dialogOption.nextCrossPoint;
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

        Debug.Log("Wait for dialog");
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
