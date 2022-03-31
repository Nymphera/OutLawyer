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
        if (state == GameState.Location)
        {
            CinemachineSwitcher.Instance.SwitchState("Biuro");
            SceneManager.LoadScene("Krabiarnia");
        }
        else if (state == GameState.Dialog)
        {
            CinemachineSwitcher.Instance.SwitchState("Biuro");
            SceneManager.LoadScene("Dialogs");
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadKrabiarnia()
    {
        CinemachineSwitcher.Instance.SwitchState("Biuro");
        SceneManager.LoadScene("Krabiarnia");
       // GameManager.Instance.UpdateGameState(GameState.Move);
    }  
    public void LoadOffice()
    {
        SceneManager.LoadScene("Biuro");
        
    }
    
   
}
