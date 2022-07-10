using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    private bool isOpen=false;
    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Help()
    {
        if (!isOpen)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isOpen = true;
        }       
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isOpen = false;
        }
    }
}
