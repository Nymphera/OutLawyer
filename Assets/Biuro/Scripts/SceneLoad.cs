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
<<<<<<< HEAD
        GameManager.Instance.UpdateGameState(GameState.Office);
        /*GameEvents.current.TriggerEvidenceUnlocked(one);
=======
        /*GameManager.Instance.UpdateGameState(GameState.Office);
        GameEvents.current.TriggerEvidenceUnlocked(one);
>>>>>>> 26c4fac (zapis dowodów w notatniku)
        GameEvents.current.TriggerEvidenceUnlocked(two);
        GameEvents.current.TriggerEvidenceUnlocked(three);
        GameEvents.current.TriggerEvidenceUnlocked(four);
        GameEvents.current.TriggerEvidenceUnlocked(five);
<<<<<<< HEAD
        GameEvents.current.TriggerEvidenceUnlocked(six);*/
=======
        GameEvents.current.TriggerEvidenceUnlocked(six);
        */
>>>>>>> 26c4fac (zapis dowodów w notatniku)
    }
   

}
