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

    

    
    
    
   

    public void Click()
    {
        OnDialogButtonClicked(dialogOption,buttonPosition);
    }
   

    public void RenderImage()
    {
        Debug.Log("Render");
        if (dialogOption.strategy == null)
        {
            Debug.Log("Strategy is null");
        }
        if (strategy1 == null)
        {
            Debug.Log("Strategy sprite is null");
        }
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
