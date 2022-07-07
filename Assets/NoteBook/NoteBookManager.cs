using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NoteBookManager : MonoBehaviour
{   
    private GameControls gameControls;
    private GameObject alert;
    private TextMeshProUGUI tmp;
    private Vector3 closePosition,openPosition;
    private bool isOpen = false;
    private void Awake()
    {
        gameControls=new GameControls();
        gameControls.Game.OpenNoteBook.performed += MenageNoteBook;
        gameControls.Game.GoBack.performed += MenageNoteBook;
    }
    private void Start()
    {       
        GameEvents.current.onEvidneceUnlocked += ShowUnlockedEvidence;
        

        alert = transform.GetChild(0).gameObject;
        alert.SetActive(false);
        closePosition = transform.GetComponent<RectTransform>().anchoredPosition;
        openPosition = new Vector3(closePosition.x, closePosition.y+200, closePosition.z);
        tmp= transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        gameControls.Enable();
    }
    private void OnDestroy()
    {
        GameEvents.current.onEvidneceUnlocked -= ShowUnlockedEvidence;
        gameControls.Game.OpenNoteBook.performed -= MenageNoteBook;
        gameControls.Game.GoBack.performed -= MenageNoteBook;
    }
    private void MenageNoteBook(InputAction.CallbackContext obj)
    {
        Debug.Log("press N");
        if (isOpen)
        {
            CloseNoteBook();
        }
        else
        {
            OpenNoteBook();
        }
    }

    private void OpenNoteBook()
    {
        isOpen = true;
        StartCoroutine(MoveUp(closePosition, openPosition));
        alert.SetActive(false);
    }

    private void CloseNoteBook()
    {
        isOpen = false;
        StartCoroutine(MoveUp(openPosition, closePosition));
    }
    private void ShowUnlockedEvidence(Evidence evidence)
    {
        Debug.Log(evidence);
        alert.SetActive(true);
        tmp.text += "\n"+evidence.Name;

    }
    private IEnumerator MoveUp(Vector2 minPosition, Vector2 maxPosition)
    {
        Vector2 pos = minPosition;
        float startTime = Time.time;
        float animationTime = 0.3f;

        while (Time.time - startTime < animationTime)
        {
            pos = Vector2.Lerp(minPosition, maxPosition, (Time.time - startTime) / animationTime);
            gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
            yield return null;
        }
        gameObject.GetComponent<RectTransform>().anchoredPosition = maxPosition;
    }

    private IEnumerator MoveDown(Vector2 minPosition, Vector2 maxPosition)
    {
        Vector2 pos = minPosition;
        float startTime = Time.time;
        float animationTime = 0.3f;

        while (Time.time - startTime < animationTime)
        {
            pos = Vector2.Lerp(minPosition, maxPosition, (Time.time - startTime) / animationTime);
            gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
            yield return null;
        }
        gameObject.GetComponent<RectTransform>().anchoredPosition = maxPosition;
    }
}
