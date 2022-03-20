using System;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{   
    public static event Action<Evidence> OnEvidenceUnlocked;

    private void OnTriggerEnter(Collider other)
    {
       
        Evidence ev= other.transform.GetComponent<InspectLogic>().evidence;
        OnEvidenceUnlocked(ev);
    }
}
