using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static event Action OnDialogEnd;
    public static DialogManager Instance;
    public Dialog dialog;
    public DialogState currentState;
    [SerializeField]
    private GameObject backGround;
    
    private Transform tree;
    [SerializeField]
    private float animationDuration=10, dialogTime = 2.5f, barIncrease = 0.2f;
    int currentLevel = 0;

    private AudioSource audioSource;
   
    
    private GameObject dialogText,Results, treeLawyer;
    private GameObject result1, result2, result3, result4, result5;

    public GameObject[] bars,predictedBars;
    private GameControls GameControls;

    private bool isDialogEnded=false;

    private void Awake()
    {
        Instance = this;
        GameControls = new GameControls();
        DialogOptionDisplay.OnDialogButtonClicked += DialogOptionDisplay_OnDialogButtonClicked;
      


        backGround.SetActive(false);
    }

   

    private void OnEnable()
    {
        GameControls.Enable();
    }
    private void OnDisable()
    {
        GameControls.Disable();
    }
    

    private void OnDestroy()
    {
        DialogOptionDisplay.OnDialogButtonClicked -= DialogOptionDisplay_OnDialogButtonClicked;
       
    }
  public void UpdateDialogState(DialogState newState)
    {
        switch(newState)
        {
            case DialogState.introduction:                
                break;
            case DialogState.playerTurn:
                break;
            case DialogState.displaySentences:
                break;
            case DialogState.victory:
                UpdateGameState();
                break;
            case DialogState.lose:
                UpdateGameState();
                break;
            case DialogState.valuate:
                break;

        }
        currentState = newState;
    }

    private void UpdateGameState()
    {
        CameraControllerKrabiarnia.Instance.SwitchState("Player");
        backGround.SetActive(false);
        for(int i = 1; i < tree.childCount; i++)
        {
            Destroy(tree.GetChild(i).gameObject);
            
        }
        Destroy(GameObject.Find("TalkingImages"));
        dialog = null;
    }

    private void Victory(int resultNumber)
    {
        
        Queue<string> sentences = new Queue<string>();
        foreach (string sentence in dialog.results[resultNumber].sentences)
        {
            sentences.Enqueue(sentence);
        }
        StartCoroutine(DisplaySentences(sentences,null));
    }

    public void StartDialog()
    {
        

        treeLawyer = GameObject.Find("lawyerIcon");     
        dialogText = GameObject.Find("DialogText");        
        backGround.SetActive(true);
        audioSource = gameObject.GetComponent<AudioSource>();
        tree = treeLawyer.transform.parent;
        Results = GameObject.Find("Results");
        Debug.Log(tree.position);
        

        ClearResultsBars();


        StartCoroutine(PlayIntroduction());
    }

    

    private void ClearResultsBars()
    {
        result1 = Results.transform.GetChild(0).gameObject;
        result2 = Results.transform.GetChild(1).gameObject;
        result3 = Results.transform.GetChild(2).gameObject;
        result4 = Results.transform.GetChild(3).gameObject;
        result5 = Results.transform.GetChild(4).gameObject;

        GameObject[] res  = new GameObject[] { result1, result2, result3, result4, result5 };
        bars = new GameObject[5];
        predictedBars = new GameObject[5];
        for(int i = 0; i < 5; i++)
        {
           bars[i] = res[i].transform.GetChild(2).gameObject;
           predictedBars[i] = res[i].transform.GetChild(1).gameObject;


        }
        for(int i = 0; i < 5; i++)
        {
            bars[i].GetComponent<Image>().fillAmount = 0.1f;
            predictedBars[i].GetComponent<Image>().fillAmount = 0.1f;
        }
    }

    private void DialogOptionDisplay_OnDialogButtonClicked(DialogOption dialogOption, Vector3 buttonPosition)
    {
            int length = dialogOption.earlierCrossPoint.ConectedDialogOptions.Length;

        for (int i = 0; i < length; i++)
        {
            if (treeLawyer.GetComponent<DialogLawyer>().currentCrossPoint == dialogOption.earlierCrossPoint)
            {
                if (dialogOption.cost == 0)
                {
                    StartCoroutine(MoveLawyer(dialogOption, buttonPosition));
                    StartDialog(dialogOption);

                    treeLawyer.GetComponent<DialogLawyer>().currentCrossPoint = dialogOption.nextCrossPoint;

                }
                else if (GameManager.Instance.keyCount >= dialogOption.cost)
                {
                    GameManager.Instance.keyCount -=dialogOption.cost;
                    UseKey();
                    GameObject obj = GameObject.Find(dialogOption.name).transform.GetChild(0).gameObject;
                    Destroy(obj);

                    StartCoroutine(MoveLawyer(dialogOption, buttonPosition));
                    StartDialog(dialogOption);

                    treeLawyer.GetComponent<DialogLawyer>().currentCrossPoint = dialogOption.nextCrossPoint;

                }
                else
                {
                    Debug.Log("You have not enough keys");

                }

            }

            
        }     
        
    }

    private void UseKey()
    {
        GameObject obj =GameObject.Find("TalkingImages");
        Transform tr = obj.transform.GetChild(4).GetChild(0);
        TextMeshProUGUI tmp = tr.GetComponent<TextMeshProUGUI>();
        tmp.text = "x" + GameManager.Instance.keyCount;
    }

    private IEnumerator PlayIntroduction()
    {
        UpdateDialogState(DialogState.introduction);
       
        
        
        
        isDialogEnded = false;
        
        Queue<string> sentences = new Queue<string>();
        Queue<AudioClip> clips = new Queue<AudioClip>();

        foreach (string sentence in dialog.introductionSentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (AudioClip clip in dialog.introductionClips)
        {
            clips.Enqueue(clip);
        }

        StartCoroutine(DisplaySentences(sentences, clips));
        yield return new WaitUntil(() => isDialogEnded == true);
       
        
    }
    private void StartDialog(DialogOption dialogOption)
    {
        UpdateDialogState(DialogState.displaySentences);

        

        isDialogEnded = false;
        Queue<string> sentences = new Queue<string>();
        Queue<AudioClip> clips = new Queue<AudioClip>();

        foreach(string sentence in dialogOption.sentences)
        {
            sentences.Enqueue(sentence);
        }
        if(dialogOption.audioClips!=null)
        foreach(AudioClip clip in dialogOption.audioClips)
        {
            clips.Enqueue(clip);
        }
        
        StartCoroutine(DisplaySentences(sentences,clips));

       

       
    }

    private IEnumerator DisplaySentences(Queue<string> sentences,Queue<AudioClip> clips)
    {
        float clipLength=0;



        while (sentences.Count != 0)
        {
           

            string sentence = sentences.Dequeue();
            if (clips.Count != 0)
            {
                AudioClip clip = clips.Dequeue();
                clipLength = clip.length;
                if (audioSource != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                }
                
            }
            

            dialogText.GetComponent<Text>().text = sentence;
            if (clipLength != 0)
                yield return new WaitForSeconds(clipLength);
            else
                yield return new WaitForSeconds(dialogTime);
        }
        
           

        
        EndDialog();
        
    }

    private void EndDialog()
    {
        dialogText.GetComponent<Text>().text = "";
        //Results.SetActive(true);
        isDialogEnded = true;
        ValuateEnding();
       
    }

    private void ValuateEnding()
    {
        UpdateDialogState(DialogState.valuate);
        for (int i = 0; i < 5; i++)
        {
           if( bars[i].GetComponent<Image>().fillAmount >= 1f)
            {
                Victory(i);
                UpdateDialogState(DialogState.victory);
                break;
            }
            
            
        }
        if (treeLawyer.GetComponent<DialogLawyer>().currentCrossPoint.ConectedDialogOptions.Length == 0)
        {
            UpdateDialogState(DialogState.lose);
        }
        else
        {
            UpdateDialogState(DialogState.playerTurn);
        }
    }

    /// <summary>
    /// Funkcja odpowiedzialna za animacjê ruchu Ikony Lawyera.
    /// </summary>
    /// <param name="dialogOption"></param>
    /// <param name="buttonPosition"></param>
    /// <returns></returns>
    private IEnumerator MoveLawyer(DialogOption dialogOption, Vector3 buttonPosition)
    {
        UpdateScore(dialogOption.strategy);

        Vector3 nextCrossPointPosition = GameObject.Find(dialogOption.nextCrossPoint.name).GetComponent<RectTransform>().localPosition;
        Vector3 newTreePosition = new Vector3(tree.localPosition.x, tree.localPosition.y - 600, tree.localPosition.z);
        float distanceToTarget = Vector3.Distance(treeLawyer.transform.localPosition, buttonPosition);
        float startTime = Time.time;

        while (distanceToTarget > 0.1f)     //move to dialog
        {

            distanceToTarget = Vector3.Distance(treeLawyer.transform.localPosition, buttonPosition);

            treeLawyer.transform.localPosition = Vector3.Lerp(treeLawyer.transform.localPosition, buttonPosition, (Time.time - startTime) / animationDuration);

            yield return null;
        }
        treeLawyer.transform.localPosition = buttonPosition;


        yield return new WaitUntil(() => isDialogEnded == true);  //wait Until

        

        startTime = Time.time;
        distanceToTarget = Vector3.Distance(treeLawyer.transform.localPosition, nextCrossPointPosition);    //przesuwanie do crosspointa
        while (distanceToTarget > 0.1f)
        {
            distanceToTarget = Vector3.Distance(treeLawyer.transform.localPosition, nextCrossPointPosition);
            treeLawyer.transform.localPosition = Vector3.Lerp(treeLawyer.transform.localPosition, nextCrossPointPosition, (Time.time - startTime) / animationDuration);
            yield return null;
        }
        treeLawyer.transform.localPosition = nextCrossPointPosition;
        yield return null;


        startTime = Time.time;
        distanceToTarget = Vector3.Distance(tree.localPosition, newTreePosition);       //przesuwanie drzewka
        while (distanceToTarget > 0.1f)
        {
            distanceToTarget = Vector3.Distance(tree.localPosition, newTreePosition);
            tree.localPosition = Vector3.Lerp(tree.localPosition, newTreePosition, (Time.time - startTime) / animationDuration);
            yield return null;
        }
        DestroyLowerLevel();

        tree.localPosition = newTreePosition;


    }

   
    private void DestroyLowerLevel()
    {
        currentLevel++;
       // Debug.Log("Current Level: " + currentLevel);
        Destroy(GameObject.Find("Level " + (currentLevel - 1)));
    }
    /// <summary>
    /// Aktualizuje wartoœci Rezultatów.
    /// </summary>
    /// <param name="strategy"></param>
    private void UpdateScore(Strategy strategy) 
    {
        Result[] updatedresults=new Result[2];

        updatedresults = GetUpadatedResults(strategy);
        StartCoroutine(AnimateResults(updatedresults));
        
        
       
    }
  
    public Result[] GetUpadatedResults(Strategy strategy)
    {
        Result[] updatedresults= new Result[2];
        int index = 0;
        foreach (Result result in dialog.results)
        {
           
            if (result.strategy1 == strategy || result.strategy2 == strategy)
            {
                
                updatedresults[index] = result;
                index++;
            }
        }
        return updatedresults;
    }
    /// <summary>
    /// Funkcja odpowiedzialna za Animowanie pasków rezultatów.
    /// </summary>
    /// <param name="updatedresults"></param>
    /// <returns></returns>
    private IEnumerator AnimateResults(Result[] updatedresults)
    {
        float startTime = Time.time;
        float firstBarValue = bars[updatedresults[0].resultNumber-1].GetComponent<Image>().fillAmount;
        float secondBarValue = bars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount;
       


                float newFirstValue = firstBarValue + barIncrease;
                float newSecondValue = secondBarValue + barIncrease;

                        float distance = barIncrease;
                        float value1,value2;
                        while (distance > 0.01f)
                        {
                            value1 = Mathf.Lerp(firstBarValue, newFirstValue, (Time.time - startTime));
                            value2 = Mathf.Lerp(secondBarValue, newSecondValue, (Time.time - startTime));

                            distance = newFirstValue - value1;

            bars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount = value1;
            bars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount = value2;
            predictedBars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount = value1;
            predictedBars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount = value2;
            yield return null;
                        }
        bars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount = newFirstValue;
        bars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount = newSecondValue;
        predictedBars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount = newFirstValue;
        predictedBars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount = newSecondValue;


        if (newFirstValue >= 1 )
                {
                    ShowResult(updatedresults[0]);
                }   
                else if (newSecondValue >= 1)
                {
                    ShowResult(updatedresults[1]);
                }
        yield return null;
    }

    private void ShowResult(Result result)
    {
        Debug.Log(result.ResultText);
    }

}
public enum DialogState
{
    introduction,
    playerTurn,
    displaySentences,
    valuate,
    victory,
    lose
}
