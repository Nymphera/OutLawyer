using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BurnLines : MonoBehaviour
{
    
    [SerializeField]
    private GameObject key;
    [SerializeField]
    private GameObject keyPrefab;
    [SerializeField]
    private Transform keyParent;
    private int keyCount = 0;
    private TextMeshProUGUI tmp;
    private void Start()
    {
        keyCount = GameManager.Instance.keyCount;
        tmp = key.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tmp.text = "x" + keyCount;
        
    }
    public void CheckAnswears()
    {
        StopAllCoroutines();
        StartCoroutine(Burn());
        tmp=key.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tmp.text = "x" + keyCount;
    }
    private IEnumerator Burn()
    {
        Transform[] childs = new Transform[transform.childCount];
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                if (transform.GetChild(transform.childCount - 1).GetComponent<Line>().secondEvidence == null)
                {
                   
                    count--;
                    childs = new Transform[transform.childCount - 1];
                }
            }
            
            childs[i] = transform.GetChild(i);
            
        }
        
           
        

        foreach (Transform child in childs)
        {
            if (!child.GetComponent<Line>().wasLineBurned) 
            {
                GameEvents.current.TriggerBurnLines(child.GetComponent<Line>());
                if (child.GetComponent<Line>().isConectionGood)
                {
                    SpawnKey(child);

                    float startTime = Time.time;
                    while (Time.time - startTime < 1)
                    {
                        Color currentColor = child.GetComponent<LineRenderer>().material.color;
                        Color lerpedColor = Color.Lerp(currentColor, Color.white, Time.time - startTime);
                        child.GetComponent<LineRenderer>().material.color = lerpedColor;
                        yield return null;
                    }
                    child.GetComponent<LineRenderer>().material.color = Color.white;
                    keyCount++;
                    tmp.text = "x" + keyCount;
                }
                else
                {
                    float startTime = Time.time;
                    while (Time.time - startTime < 1)
                    {
                        Color currentColor = child.GetComponent<LineRenderer>().material.color;
                        Color lerpedColor = Color.Lerp(currentColor, Color.black, Time.time - startTime);
                        child.GetComponent<LineRenderer>().material.color = lerpedColor;
                        yield return null;
                    }
                    child.GetComponent<LineRenderer>().material.color = Color.black;

                }
                child.GetComponent<Line>().wasLineBurned = true;
                
            }
            
        }
        GameManager.Instance.keyCount = keyCount;
        PinBoardManager.Instance.CursorToNeutral();
    }

    private void SpawnKey(Transform child)
    {
        Vector3[] vectors = new Vector3[2];
        vectors[0]=child.GetComponent<LineRenderer>().GetPosition(0);
        vectors[1]=child.GetComponent<LineRenderer>().GetPosition(1);
        Vector3 keyPosition=Vector3.Lerp(vectors[0], vectors[1], 0.5f);
       
        GameObject key = Instantiate(keyPrefab, keyParent);
        keyPosition = Camera.main.WorldToScreenPoint(keyPosition);
        keyPosition.y += 20f;
        key.GetComponent<RectTransform>().position = keyPosition; 
        key.GetComponent<KeyButton>().explenation = child.GetComponent<Line>().Conclusion;
    }
}
