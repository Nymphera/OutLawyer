using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField]
    private bool startDialog=false;
    private bool wasDialogPlayed=false;
    public static event Action<Dialog> OnTriggerDialog;
    [SerializeField]
    private Dialog dialogToTrigger;
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log(dialogToTrigger.name);

        startDialog = true;
        
    }
    private void Update()
    {
        
        if (startDialog&&!wasDialogPlayed)
        {
            wasDialogPlayed = true;
            startDialog = false;
            StartCoroutine(StartDialog());
     
        }
        
    }

    private IEnumerator StartDialog()
    {
        CameraControllerKrabiarnia.Instance.SwitchState(dialogToTrigger.name);


       // GameManager.Instance.UpdateGameState(GameState.LockInteract);

        yield return new WaitForSeconds(2f);
        GameObject dialogTree = GameObject.Find("DialogTree");
        dialogTree.GetComponent<DialogTreeCreator>().dialog = dialogToTrigger;
        dialogTree.GetComponent<DialogTreeCreator>().CreateTree();

        GameObject dialogManager = GameObject.Find("DialogManager");
        dialogManager.GetComponent<DialogManager>().dialog = dialogToTrigger;
        dialogManager.GetComponent<DialogManager>().StartDialog();
    }
}
