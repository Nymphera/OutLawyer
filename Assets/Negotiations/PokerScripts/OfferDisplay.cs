using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OfferDisplay : MonoBehaviour
{
    [SerializeField]
    public  Offer offer;
    
    private void Awake()
    {
        SetOffers();
    }

    private void SetOffers()
    {
      TextMeshProUGUI tmp=  transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tmp.text = offer.offerText;
        TextMeshProUGUI tmp2 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        tmp2.text = offer.offerValue.ToString();
        TextMeshProUGUI tmp3 = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        tmp3.text = offer.offerValue.ToString();
    }
    public void PlayDialog(string[] messages)
    {

        Queue<string> sentences = new Queue<string>();
        foreach (string sentence in messages)
        {
            sentences.Enqueue(sentence);
        }

        StartCoroutine(DisplaySentences(sentences));
    }

    private IEnumerator DisplaySentences(Queue<string> sentences)
    {

        TextMeshProUGUI tmp = GameObject.Find("DialogOutputNegotiations").GetComponent<TextMeshProUGUI>();
        while (sentences.Count != 0)
        {
            string sentence = sentences.Dequeue();
            tmp.text = sentence;
            yield return new WaitForSeconds(3f);
        }
        tmp.text = "";
    }
}
