using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{[SerializeField]
    private GameObject dontDestroyOnLoad;
    private void Start()
    {
       // DontDestroyOnLoad(dontDestroyOnLoad);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadKrabiarnia()
    {
        
        SceneManager.LoadScene("Krabiarnia");
    }  
    public void LoadOffice()
    {
        SceneManager.LoadScene("Game");
        
    }
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Biuro");
    }
}
