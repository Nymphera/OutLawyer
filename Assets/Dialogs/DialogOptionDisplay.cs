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
    
    
    private Image lawyerIcon;
    WaitForSeconds wait = new WaitForSeconds(1f);
    float animationDuration =3f;
    Coroutine moveCorountine;
   
    public void Click()
    {
        
        StartCoroutine(MoveLawyer());
    }
    private IEnumerator MoveLawyer()    //nie wiem czy to powinno byæ tutaj
    {
        lawyerIcon = GameObject.Find("LawyerImage(Clone)").GetComponent<Image>();
        Transform tree = lawyerIcon.transform.parent;
        
       

        float distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, buttonPosition);
        
        while (distanceToTarget > 0.1f)
        {   
            distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, buttonPosition);

            lawyerIcon.rectTransform.localPosition = Vector3.Lerp(lawyerIcon.rectTransform.localPosition, buttonPosition, 0.01f);
            
            yield return null;
        }
        lawyerIcon.rectTransform.localPosition = buttonPosition;
        Debug.Log("Wait for dialog");
        yield return wait;  //wait Until
        Vector3 nextCrossPointPosition = GameObject.Find(dialogOption.nextCrossPoint.name).GetComponent<RectTransform>().localPosition;
        Vector3 newTreePosition = new Vector3(tree.localPosition.x, tree.localPosition.y - 600, tree.localPosition.z);
        distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition);
        while (distanceToTarget > 0.1f)
        {
            distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition);
            lawyerIcon.rectTransform.localPosition = Vector3.Lerp(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition, 0.01f);
            
            tree.localPosition= Vector3.Lerp(tree.localPosition, newTreePosition, 0.01f);


            yield return null;


        }
        lawyerIcon.rectTransform.localPosition = nextCrossPointPosition;
        tree.localPosition = newTreePosition;

        /*Vector2 currentposition = lawyerIcon.rectTransform.position;
       
        float elapsedTime = 0;
        
        while (elapsedTime<animationDuration)
        {
            lawyerIcon.rectTransform.localPosition = Vector3.Lerp(lawyerIcon.rectTransform.localPosition, buttonPosition, elapsedTime/animationDuration);
            elapsedTime += Time.deltaTime;
             
            yield return null;
        }
        Debug.Log("done");
        lawyerIcon.rectTransform.localPosition = buttonPosition;

        yield return wait;  //tu bêdzie czekanie a¿ siê zrobi dialog
      
        
        
        Vector3 nextCrossPointPosition=GameObject.Find(dialogOption.nextCrossPoint.name).GetComponent<RectTransform>().localPosition;
        Vector3 newTreePosition = new Vector3(tree.localPosition.x, tree.localPosition.y - 600, tree.localPosition.z);

        
        elapsedTime = 0;
        while (elapsedTime>animationDuration)      //jednoczeœnie animuje przejœcie po dialogu i drzewo
         {

            lawyerIcon.rectTransform.localPosition = Vector3.Lerp(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition, elapsedTime/animationDuration);
             
            tree.localPosition = Vector3.Lerp(tree.localPosition,newTreePosition, elapsedTime/animationDuration);
            
            
            yield return null;
         }
        lawyerIcon.rectTransform.localPosition = nextCrossPointPosition;
        tree.localPosition = newTreePosition;
        */



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
