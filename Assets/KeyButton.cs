using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KeyButton : MonoBehaviour
{
    public string explenation;
    
    private TextMeshProUGUI tmp;
    private void Awake()
    {
        tmp = GameObject.Find("messageText").GetComponent<TextMeshProUGUI>();   
    }
    public void ShowExplenation()
    {
        StartCoroutine(Explenation());
        
        
    }

    private IEnumerator Explenation()
    {
        tmp.text = explenation;
        yield return new WaitForSeconds(3f);
        tmp.text = "";
    }
}
