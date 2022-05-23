using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }
    private void OnTriggerEnter(Collider other)
    {
        LoadOffice();
    }
    private void GameManager_OnGameStateChanged(GameState state)
    {
        if (state == GameState.Move)
        {
           // CinemachineSwitcher.Instance.SwitchState("Biuro");
            //SceneManager.LoadScene("Krabiarnia");
        }
        else if (state == GameState.LockInteract)
        {
            //CinemachineSwitcher.Instance.SwitchState("Biuro");
           // SceneManager.LoadScene("Dialogs");
        }
    }

    public void LoadKrabiarnia()
    {
        GameEvents.current.OfficeClick(0);
        SceneManager.LoadScene("Krabiarnia");
        GameManager.Instance.UpdateGameState(GameState.Move);
    }  
    public void LoadOffice()
    {
        SceneManager.LoadScene("Biuro");
        GameManager.Instance.UpdateGameState(GameState.Office);
    }
    
   
}
