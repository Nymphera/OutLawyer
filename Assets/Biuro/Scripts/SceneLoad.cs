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
        GameEvents.current.TriggerEvidenceUnlocked(one);
        
    }
 
    public void LoadOffice()
    {
        SceneManager.LoadScene("Biuro");
        GameManager.Instance.UpdateGameState(GameState.Office);
        GameEvents.current.TriggerEvidenceUnlocked(two);
    }
   

}
