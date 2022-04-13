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

    public Dialog dialog;

    
    
    private Transform tree;
    [SerializeField]
    private float animationDuration=10, dialogTime = 2f, barIncrease = 0.2f;
    int currentLevel = 0;

    private AudioSource audioSource;
   
    
    private GameObject lawyerBubble,dialogText,Results, treeLawyer,Lawyer, currentRaycastObject, lawyerText;
    private GameObject result1, result2, result3, result4, result5;

    private GameObject[] bars;
    private GameControls GameControls;

    private bool isDialogEnded=false;

    private void Awake()
    {   
        GameControls = new GameControls();
        DialogOptionDisplay.OnDialogButtonClicked += DialogOptionDisplay_OnDialogButtonClicked;
        GameControls.Game.MousePosition.performed += MousePosition_performed;   

    }

   

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        GameControls.Disable();
    }
    

    private void OnDestroy()
    {
        DialogOptionDisplay.OnDialogButtonClicked -= DialogOptionDisplay_OnDialogButtonClicked;
        GameControls.Game.MousePosition.performed -= MousePosition_performed;
    }
  
    public void StartDialog()
    {
        GameControls.Enable();
        treeLawyer = GameObject.Find("lawyerIcon");
        Lawyer = GameObject.Find("LawyerImage");
        lawyerBubble = Lawyer.transform.GetChild(0).gameObject;
        dialogText = GameObject.Find("DialogText");
        lawyerText = lawyerBubble.transform.GetChild(0).gameObject;
        
        audioSource = gameObject.GetComponent<AudioSource>();
        
        tree = treeLawyer.transform.parent;
        Results = GameObject.Find("Results");
        lawyerBubble.SetActive(false);
        dialogText.SetActive(false);
       
        
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
        for(int i = 0; i < 5; i++)
        {
           bars[i] = res[i].transform.GetChild(1).gameObject;
        }
        for(int i = 0; i < 5; i++)
        {
            bars[i].GetComponent<Image>().fillAmount = 0.1f;
        }
    }

    private void DialogOptionDisplay_OnDialogButtonClicked(DialogOption dialogOption, Vector3 buttonPosition)
    {

        if (GameManager.Instance.isInputEnabled)
        {

            int length = dialogOption.earlierCrossPoint.ConectedDialogOptions.Length;

            for (int i = 0; i < length; i++)
            {
                if (treeLawyer.GetComponent<DialogLawyer>().currentCrossPoint == dialogOption.earlierCrossPoint)
                {
                    StartCoroutine(MoveLawyer(dialogOption, buttonPosition));
                    StartDialog(dialogOption);


                    treeLawyer.GetComponent<DialogLawyer>().currentCrossPoint = dialogOption.nextCrossPoint;




                }
            }
        }
       
        
    }
    private IEnumerator PlayIntroduction()
    {
        GameControls.Disable();
       // Results.SetActive(false);
        dialogText.SetActive(true);
        lawyerBubble.SetActive(false);
        GameControls.Disable();
        
        isDialogEnded = false;
        GameManager.Instance.isInputEnabled = false;
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
        GameControls.Enable();
        GameManager.Instance.isInputEnabled = true;
    }
    private void StartDialog(DialogOption dialogOption)
    {
        //Results.SetActive(false);
        dialogText.SetActive(true);
        lawyerBubble.SetActive(false);
        isDialogEnded = false;
        Queue<string> sentences = new Queue<string>();
        Queue<AudioClip> clips = new Queue<AudioClip>();

        foreach(string sentence in dialogOption.sentences)
        {
            sentences.Enqueue(sentence);
        }
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
            GameControls.Disable();

            string sentence = sentences.Dequeue();
            if (clips.Count != 0)
            {
                AudioClip clip = clips.Dequeue();
                clipLength = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
            }
            

            dialogText.GetComponent<Text>().text = sentence;
            if (clipLength != 0)
                yield return new WaitForSeconds(clipLength);
            else
                yield return new WaitForSeconds(dialogTime);
        }
        
            GameControls.Enable();

        
        EndDialog();
        
    }

    private void EndDialog()
    {
        dialogText.SetActive(false);
        //Results.SetActive(true);
        isDialogEnded = true;
        
    }

    /// <summary>
    /// Funkcja odpowiedzialna za animacjê ruchu Ikony Lawyera.
    /// </summary>
    /// <param name="dialogOption"></param>
    /// <param name="buttonPosition"></param>
    /// <returns></returns>
    private IEnumerator MoveLawyer(DialogOption dialogOption, Vector3 buttonPosition)
    {


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

        UpdateScore(dialogOption.strategy);

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

    /// <summary>
    /// Ustawia wartoœæ <b>ResultBar</b> na 0.
    /// </summary>
    
    /// <summary>
    /// <b>GetResults</b>
    /// Przypisuje zmiennym result1,result2,...wartoœci <code>Result</code>
    /// </summary>
    
    /// <summary>
    /// Tu mo¿na zniszczyæ odpowiednie linie dialogów, mo¿na te¿ zniszczyæ opcje których ju¿ nie mo¿emy wybraæ gdyby ktoœ chcia³.
    /// </summary>
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
        
        switch (strategy)
  
        {
            case Strategy.LuŸnaGadka:
                updatedresults = GetUpadatedResults(strategy);
                StartCoroutine(AnimateResults(updatedresults));
                        break;
            case Strategy.Podstêp:
                updatedresults = GetUpadatedResults(strategy);
                StartCoroutine(AnimateResults(updatedresults));
                break;
            case Strategy.Profesjonalizm:
                updatedresults = GetUpadatedResults(strategy);
                StartCoroutine(AnimateResults(updatedresults));
                break;
            case Strategy.UrokOsobisty:
                updatedresults = GetUpadatedResults(strategy);
                StartCoroutine(AnimateResults(updatedresults));
                break;
            case Strategy.ZimnaKrew:
                updatedresults = GetUpadatedResults(strategy);
                StartCoroutine(AnimateResults(updatedresults));
                break;

            default:
                break;
        }
       
    }
    /// <summary>
    /// Zwraca tablice z aktualizowanymi paskami rezultatów.
    /// </summary>
    /// <param name="strategy"></param>
    /// <returns></returns>
    private Result[] GetUpadatedResults(Strategy strategy)
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
                            yield return null;
                        }
        bars[updatedresults[0].resultNumber - 1].GetComponent<Image>().fillAmount = newFirstValue;
        bars[updatedresults[1].resultNumber - 1].GetComponent<Image>().fillAmount = newSecondValue;
        
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

   
    private void MousePosition_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (GameManager.Instance.isInputEnabled)
        {
            lawyerBubble.SetActive(IsPointerOverUIElement());

            if (IsPointerOverUIElement())
            {
                GameObject obj = currentRaycastObject;
                DialogOption option = obj.GetComponent<DialogOptionDisplay>().dialogOption;
                lawyerText.GetComponent<Text>().text = option.text;
            }
        }
    }

    /// <summary>
    /// Returns 'true' if we touched or hovering on Unity UI element.
    /// </summary>
    /// <returns></returns>
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    /// <summary>
    /// Returns 'true' if we touched or hovering on Unity UI element.
    /// </summary>
    /// <param name="eventSystemRaysastResults"></param>
    /// <returns></returns>
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == 10)
            {
                currentRaycastObject = curRaysastResult.gameObject;
                return true;
            }   //10 is UI layer int

        }
        currentRaycastObject = null;
        return false;
    }


    /// <summary>
    /// Gets all event system raycast results of current mouse or touch position.
    /// </summary>
    /// <returns></returns>
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
