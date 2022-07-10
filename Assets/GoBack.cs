using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    GameObject canvas;
    private void Awake()
    {
        canvas= transform.GetChild(0).gameObject;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        showPanel();
    }
    public void showPanel()
    {
        canvas.SetActive(true);
        GameManager.Instance.UpdateGameState(GameState.Interact);
    }
    public void hidePanel()
    {
        canvas.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Move);
    }
    public void goBackToOffice()
    {
        GameManager.Instance.UpdateGameState(GameState.Office);
        SceneManager.LoadScene("Biuro");
        
    }
}
