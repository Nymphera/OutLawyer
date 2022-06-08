using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    GameControls gameControls;
    GameObject pausePanel;
    private bool panelState;
    private void Awake()
    {
        gameControls = new GameControls();
        gameControls.Game.GoBack.performed += GoBack_performed;
        pausePanel = transform.GetChild(0).gameObject;
        pausePanel.SetActive(false);
    }

    private void GoBack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.CurrentState == GameState.Move || GameManager.Instance.CurrentState == GameState.Office)
        {
            
            if (pausePanel.activeSelf)
            {
                GoBackToGame();
            }
            else
            {
                ShowPanel();              
            }
            
            
        }
    }
    private void ShowPanel()
    {
        GameManager.Instance.UpdateGameState(GameState.Office);
        GameManager.Instance.UpdateGameState(GameState.LockInteract);
        pausePanel.SetActive(true);
        
    }
    private void OnEnable()
    {
        gameControls.Enable();
    }
    private void OnDestroy()
    {
        gameControls.Game.GoBack.performed -= GoBack_performed;
    }
    
    public void GoBackToGame()
    {
        if (SceneManager.GetActiveScene().name == "Biuro")
        {
            GameManager.Instance.UpdateGameState(GameState.Office);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.Move);
        }
           
        pausePanel.SetActive(false);
    }
    public void EndGame()
    {
        pausePanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
