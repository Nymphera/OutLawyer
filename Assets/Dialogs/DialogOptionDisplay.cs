using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogOptionDisplay : MonoBehaviour
{
    public DialogOption dialogOption;
    public Vector2 position;
    [SerializeField]
    private Sprite strategy1, strategy2, strategy3, strategy4, strategy5;
    
    private Image lawyerIcon;
    private void Awake()
    {
        
    }
    public void Click()
    {
        lawyerIcon = GameObject.Find("LawyerImage(Clone)").GetComponent<Image>();
        lawyerIcon.rectTransform.localPosition = position;
    }

    public void RenderImage()
    {
        
        if (dialogOption.strategy == Strategy.ZimnaKrew)
        {
            GetComponent<Image>().sprite = strategy1;
        }
        else
                    if (dialogOption.strategy == Strategy.Podst�p)
        {
            GetComponent<Image>().sprite = strategy2;
        }
        else
                    if (dialogOption.strategy == Strategy.Profesjonalizm)
        {
            GetComponent<Image>().sprite = strategy3;
        }
        else
                    if (dialogOption.strategy == Strategy.Lu�naGadka)
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
