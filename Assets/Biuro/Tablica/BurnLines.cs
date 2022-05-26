using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BurnLines : MonoBehaviour
{
    
    [SerializeField]
    private GameObject key;
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
        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
        
        foreach(Transform child in childs)
        {
            if (!child.GetComponent<Line>().wasLineBurned) 
            {
                GameEvents.current.TriggerBurnLines(child.GetComponent<Line>());
                if (child.GetComponent<Line>().isConectionGood)
                {
                    

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
    }
}
