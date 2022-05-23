using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        LoadOffice();
    }
 
    public void LoadOffice()
    {
        SceneManager.LoadScene("Biuro");
        GameManager.Instance.UpdateGameState(GameState.Office);
    }
   

}
