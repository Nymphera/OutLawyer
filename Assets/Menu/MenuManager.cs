using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject SettingsPanel;
    GameControls GetInputActions;
    private void Awake()
    {
        GetInputActions = new GameControls();
         SettingsPanel = GameObject.Find("Settings");
        SettingsPanel.SetActive(false);
        GetInputActions.Game.GoBack.performed += GoBack_performed;
    }
    private void OnEnable()
    {
        GetInputActions.Enable();
    }
    private void OnDisable()
    {
        GetInputActions.Disable();
    }

    private void GoBack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SettingsPanel.SetActive(false);
    }

    private void Start()
    {
       
    }
    public void CloseAplication()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Biuro");
    }
    public void ShowSettings()
    {
        SettingsPanel.SetActive(true);
    }
}
