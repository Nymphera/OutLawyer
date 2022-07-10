using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    GameControls gameControls;
    [SerializeField]
    private GameObject pausePanel;
    private bool panelState;
    private void Awake()
    {
        gameControls = new GameControls();
        gameControls.Game.GoBack.performed += GoBack_performed;

        if (Instance != null)
        {
            Destroy(gameObject);
            
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        pausePanel = transform.GetChild(0).gameObject;
        pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        gameControls.Enable();
    }
    private void OnDestroy()
    {
        gameControls.Game.GoBack.performed -= GoBack_performed;
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
              StartCoroutine ( ShowPanel());              
            }       
        }
        if(pausePanel.activeSelf&& GameManager.Instance.CurrentState == GameState.LockInteract)
        {
             GoBackToGame();
            Debug.Log("go back2");
        }
    }
    private IEnumerator ShowPanel()
    {    
        yield return null;
        pausePanel.SetActive(true);
        GameManager.Instance.UpdateGameState(GameState.LockInteract);
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
