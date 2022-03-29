using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DialogOptionDisplay : MonoBehaviour
{
    public DialogOption dialogOption;
    public Vector2 position;
    [SerializeField]
    private Sprite strategy1, strategy2, strategy3, strategy4, strategy5;
    
    
    private Image lawyerIcon;
    WaitForSeconds wait = new WaitForSeconds(5f);
    float animationDuration =3f;
   
    public void Click()
    {
        StartCoroutine(MoveLawyer());
    }
    private IEnumerator MoveLawyer()
    {
        float startTime=Time.time;
        lawyerIcon = GameObject.Find("LawyerImage(Clone)").GetComponent<Image>();
        Vector2 pos = lawyerIcon.rectTransform.localPosition;
        while (pos!= position)
        {
            pos = Vector2.Lerp(lawyerIcon.rectTransform.localPosition, position, (Time.time - startTime) / animationDuration);
            lawyerIcon.rectTransform.localPosition = pos;
            yield return null;
        }
        lawyerIcon.rectTransform.localPosition = position;

        yield return wait;  //tu bêdzie czekanie a¿ siê zrobi dialog
        startTime = Time.time;
        pos = lawyerIcon.rectTransform.localPosition;
        Vector2 pos2=GameObject.Find(dialogOption.nextCrossPoint.name).GetComponent<RectTransform>().localPosition;

        Transform tree = lawyerIcon.transform.parent;
        //tree.localPosition = new Vector3(tree.localPosition.x, tree.localPosition.y - 600, tree.localPosition.z);
        Vector3 newTreePosition = new Vector3(tree.localPosition.x, tree.localPosition.y - 600, tree.localPosition.z);
        Vector3 pos3;
        while (pos!= pos2)
         {
            Debug.Log(Time.time);
            pos = Vector2.Lerp(lawyerIcon.rectTransform.localPosition, pos2, (Time.time - startTime)/animationDuration);
            lawyerIcon.rectTransform.localPosition = pos;

            pos3=Vector3.Lerp(tree.localPosition,newTreePosition, (Time.time - startTime) / animationDuration);
            tree.localPosition = pos3;
            yield return null;
         }
        lawyerIcon.rectTransform.localPosition = pos2;
        
        
        
        
        
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
