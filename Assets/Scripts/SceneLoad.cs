using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadKrabiarnia()
    {
       
        SceneManager.LoadScene("Krabiarnia");
        GameManager.Instance.UpdateGameState(GameState.Move);
    }  
    public void LoadOffice()
    {
        SceneManager.LoadScene("Game");
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        SceneManager.LoadScene("Biuro");
        GameManager.Instance.UpdateGameState(GameState.Office);
    }
}
