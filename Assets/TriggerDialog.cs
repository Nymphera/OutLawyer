using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField]
    private bool startDialog=false;
    public static event Action<Dialog> OnTriggerDialog;
    [SerializeField]
    private Dialog dialogToTrigger;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(dialogToTrigger.name);

        
    }
    private void Update()
    {
        
        if (startDialog)
        {
            startDialog = false;
            GameObject dialogTree = GameObject.Find("DialogTree");
            dialogTree.GetComponent<DialogTreeCreator>().dialog = dialogToTrigger;
            dialogTree.GetComponent<DialogTreeCreator>().CreateTree();

            GameObject dialogManager = GameObject.Find("DialogManager");
            dialogManager.GetComponent<DialogManager>().dialog = dialogToTrigger;
            dialogManager.GetComponent<DialogManager>().StartDialog();
            
            
        }
        
    }
}
