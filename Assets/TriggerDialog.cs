using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public static event Action<Dialog> OnTriggerDialog;
    [SerializeField]
    private Dialog dialogToTrigger;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(dialogToTrigger.name);

        OnTriggerDialog(dialogToTrigger);
    }
}
