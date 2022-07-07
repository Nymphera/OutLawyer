using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    Evidence one, two,three,four,five,six,seven,eight,nine,ten,eleven,twelve;
    private void OnTriggerEnter(Collider other)
    {
        LoadOffice();
        
        
    }
 
    public void LoadOffice()
    {
        SceneManager.LoadScene("Biuro");
        GameManager.Instance.UpdateGameState(GameState.Office);
        /*GameEvents.current.TriggerEvidenceUnlocked(one);
        GameEvents.current.TriggerEvidenceUnlocked(two);
        GameEvents.current.TriggerEvidenceUnlocked(three);
        GameEvents.current.TriggerEvidenceUnlocked(four);
        GameEvents.current.TriggerEvidenceUnlocked(five);
        GameEvents.current.TriggerEvidenceUnlocked(six);*/
    }
   

}
