using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrologTrigger : MonoBehaviour
{
    [SerializeField, TextArea(3,10)]
    string[] sentences;
    [SerializeField]
    private Evidence[] evidences;
    TextMeshProUGUI tmp;
    [SerializeField]
    private bool playProlog=true;
    private bool isDialogEnded=false;
    private int countSentences=0;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (!isDialogEnded)
        {
            transform.GetComponent<AudioSource>().Stop();
            transform.GetComponent<Outline>().OutlineWidth = 0;
            StartCoroutine(Prolog());
        }
       
    }

    private IEnumerator Prolog()
    {
        
        transform.GetChild(0).gameObject.SetActive(true);
        yield return null;
        StartCoroutine(DisplaySenstences(sentences));
        yield return new WaitUntil(() => isDialogEnded == true);
        transform.GetChild(0).gameObject.SetActive(false);
       
       foreach(Evidence evidence in evidences)
        {
            GameEvents.current.TriggerEvidenceUnlocked(evidence);
        }
        GameManager.Instance.UpdateGameState(GameState.Office);
    }

    private IEnumerator DisplaySenstences(string[] sentences)
    {
        tmp = GameObject.Find("prologText").GetComponent<TextMeshProUGUI>();
        Queue<string> queue = new Queue<string>();
        foreach(string sentence in sentences)
        {
            queue.Enqueue(sentence);
        }
        while(queue.Count > 0&&playProlog)
        { 
            countSentences++;
            tmp.text += "\n"+queue.Dequeue();
            tmp.text+="\n";
            yield return new WaitForSeconds(3f);
            if (countSentences > 9)
            {
                tmp.text = "";
                countSentences = 0;
            }
        }
        isDialogEnded = true;
    }
    public void SkipProlog()
    {
        playProlog = false;
    }
}
