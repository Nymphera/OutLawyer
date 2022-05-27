using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadKrabiarnia : MonoBehaviour
{
    SceneLoad load;
    
    void Start()
    {
        SceneManager.LoadScene("Krabiarnia");
        GameManager.Instance.UpdateGameState(GameState.Move);
    }

    
}
