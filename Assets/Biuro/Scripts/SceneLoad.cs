using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    Evidence one, two;
    private void OnTriggerEnter(Collider other)
    {
        LoadOffice();
        
        
    }
 
    public void LoadOffice()
    {
        SceneManager.LoadScene("Biuro");
        GameManager.Instance.UpdateGameState(GameState.Office);
        GameEvents.current.TriggerEvidenceUnlocked(one);
        GameEvents.current.TriggerEvidenceUnlocked(two);
    }
   

}
