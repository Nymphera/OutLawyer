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
    int currentLevel=0;

    private Image lawyerIcon;
    WaitUntil wait;
    Transform tree;
    [SerializeField]
    private float animationDuration=10;
    
    private GameObject Lawyer;
    private AudioSource audioSource;
    [SerializeField]
    private float barIncrease = 0.2f;
    [HideInInspector]
    [SerializeField]
    private Result result1, result2, result3, result4, result5;
    private Result[] results;
    [SerializeField]
    private GameObject lawyerBubbleText,dialogText,Results;
    private GameControls GameControls;
   
    private GameObject currentRaycastObject,lawyerText;

    private bool isDialogEnded=false;
    private void Awake()
    {
        lawyerText = lawyerBubbleText.transform.GetChild(0).gameObject;
        GameControls = new GameControls();
        audioSource = gameObject.GetComponent<AudioSource>();
        GetResults();
        ClearResultsBars();
        

        DialogOptionDisplay.OnDialogButtonClicked += DialogOptionDisplay_OnDialogButtonClicked;
        GameControls.Game.MousePosition.performed += MousePosition_performed;
        
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
        GameControls.Game.MousePosition.performed -= MousePosition_performed;
    }
    private void Start()
    {
        lawyerBubbleText.SetActive(false);
        dialogText.SetActive(false);
        Lawyer = GameObject.Find("LawyerImage(Clone)");
        lawyerIcon = Lawyer.GetComponent<Image>();
        tree = Lawyer.transform.parent;
    }

    private void DialogOptionDisplay_OnDialogButtonClicked(DialogOption dialogOption, Vector3 buttonPosition)
    {
       

        int length=dialogOption.earlierCrossPoint.ConectedDialogOptions.Length;
        
       for (int i = 0; i < length; i++)
        {
            if (Lawyer.GetComponent<DialogLawyer>().currentCrossPoint == dialogOption.earlierCrossPoint)
            {
                StartCoroutine(MoveLawyer(dialogOption, buttonPosition));
                StartDialog(dialogOption);

                
                Lawyer.GetComponent<DialogLawyer>().currentCrossPoint = dialogOption.nextCrossPoint;

                
                
                
            }
        }

       
        
    }

    private void StartDialog(DialogOption dialogOption)
    {
        Results.SetActive(false);
        dialogText.SetActive(true);
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
                yield return new WaitForSeconds(2f);
        }
        
            GameControls.Enable();
        

        EndDialog();
       
    }

    private void EndDialog()
    {
        dialogText.SetActive(false);
        Results.SetActive(true);
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
        float distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, buttonPosition);
        float startTime = Time.time;

        while (distanceToTarget > 0.1f)     //move to dialog
        {

            distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, buttonPosition);

            lawyerIcon.rectTransform.localPosition = Vector3.Lerp(lawyerIcon.rectTransform.localPosition, buttonPosition, (Time.time - startTime) / animationDuration);

            yield return null;
        }
        lawyerIcon.rectTransform.localPosition = buttonPosition;


        yield return new WaitUntil(() => isDialogEnded == true);  //wait Until

        UpdateScore(dialogOption.strategy);

        startTime = Time.time;
        distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition);    //przesuwanie do crosspointa
        while (distanceToTarget > 0.1f)
        {
            distanceToTarget = Vector3.Distance(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition);
            lawyerIcon.rectTransform.localPosition = Vector3.Lerp(lawyerIcon.rectTransform.localPosition, nextCrossPointPosition, (Time.time - startTime) / animationDuration);
            yield return null;
        }
        lawyerIcon.rectTransform.localPosition = nextCrossPointPosition;
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
    private void ClearResultsBars()
    {

        foreach(Result result in results)
        {
            result.ResultBar.gameObject.GetComponent<Image>().fillAmount = 0.1f;
        }
        
    }
    /// <summary>
    /// <b>GetResults</b>
    /// Przypisuje zmiennym result1,result2,...wartoœci <code>Result</code>
    /// </summary>
    private void GetResults()
    {
        Transform resultsTrans = GameObject.Find("Results").transform;
        
        result1 = resultsTrans.GetChild(0).gameObject.GetComponent<Result>();
        result2 = resultsTrans.GetChild(1).gameObject.GetComponent<Result>();
        result3 = resultsTrans.GetChild(2).gameObject.GetComponent<Result>();
        result4 = resultsTrans.GetChild(3).gameObject.GetComponent<Result>();
        result5 = resultsTrans.GetChild(4).gameObject.GetComponent<Result>();
        results = new Result[] { result1, result2, result3, result4, result5 };
    }
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
        foreach (Result result in results)
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

        float startFirstValue= updatedresults[0].ResultBar.GetComponent<Image>().fillAmount;
        float startSecondValue = updatedresults[1].ResultBar.GetComponent<Image>().fillAmount;


        float newFirstValue = startFirstValue + barIncrease;
        float newSecondValue = startSecondValue + barIncrease;
                
                float distance = barIncrease;
                float value1,value2;
                while (distance > 0.01f)
                {
                    value1 = Mathf.Lerp(startFirstValue, newFirstValue, (Time.time - startTime));
                    value2 = Mathf.Lerp(startSecondValue, newSecondValue, (Time.time - startTime));

                    distance = newFirstValue - value1;

                    updatedresults[0].ResultBar.GetComponent<Image>().fillAmount = value1;
                    updatedresults[1].ResultBar.GetComponent<Image>().fillAmount = value2;
                    yield return null;
                }
                updatedresults[0].ResultBar.GetComponent<Image>().fillAmount = newFirstValue;   
                updatedresults[1].ResultBar.GetComponent<Image>().fillAmount = newSecondValue;
        if (newFirstValue >= 1 )
        {
            ShowResult(updatedresults[0]);
        }   
        else if (newSecondValue >= 1)
        {
            ShowResult(updatedresults[1]);
        }
    }

    private void ShowResult(Result result)
    {
        Debug.Log(result.ResultText);
    }

   
    private void MousePosition_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        lawyerBubbleText.SetActive(IsPointerOverUIElement());
        
        if (IsPointerOverUIElement())
        {
            GameObject obj = currentRaycastObject;
            DialogOption option = obj.GetComponent<DialogOptionDisplay>().dialogOption;
            lawyerText.GetComponent<Text>().text = option.text;
        };
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
