using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class NewGameClick : MonoBehaviour
{
    public EventTrigger CreditsTrigger; //Assign credits hover trigger here.
    public EventTrigger ContinueTrigger; //Assign continue hover trigger here.
    public EventTrigger NewGameTrigger; //Assign new game hover trigger here.
    public EventTrigger OptionsTrigger; //Assign options trigger here.
    public EventTrigger ExitTrigger; //Assign exit trigger here.
    public GameObject WarningUI; //Assign warning UI element here.
    
    public void disableTriggers() {
        //Disable all triggers, enable warning UI.
        CreditsTrigger.enabled = false;
        ContinueTrigger.enabled = false;
        NewGameTrigger.enabled = false;
        OptionsTrigger.enabled = false;
        ExitTrigger.enabled = false;
        WarningUI.SetActive(true);
    }

    public void enableTriggers() {
        //Enable all triggers, disable warning UI.
        CreditsTrigger.enabled = true;
        ContinueTrigger.enabled = true;
        NewGameTrigger.enabled = true;
        OptionsTrigger.enabled = true;
        ExitTrigger.enabled = true;
        WarningUI.SetActive(false);
    }
   
}
