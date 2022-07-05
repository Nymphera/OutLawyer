using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    GameControls gameControls;
    GameObject pausePanel;
    private bool panelState;
    private void Awake()
    {
        gameControls = new GameControls();
        gameControls.Game.GoBack.performed += GoBack_performed;
        pausePanel = transform.GetChild(0).gameObject;
        pausePanel.SetActive(false);
        if (Instance != null)
        {
            Destroy(gameObject);
            
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
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
                StartCoroutine(ShowPanel());              
            }       
        }
        if(pausePanel.activeSelf&& GameManager.Instance.CurrentState == GameState.LockInteract)
        {
            GoBackToGame();
        }
    }
    private IEnumerator ShowPanel()
    { 
            pausePanel.SetActive(true);
            yield return null;
            GameManager.Instance.UpdateGameState(GameState.LockInteract);        
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
