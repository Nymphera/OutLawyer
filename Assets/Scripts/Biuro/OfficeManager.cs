using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    private GameObject PinBoard,GameManager;
    private PinBoardLogic pinBoardLogic;
    private Interact interact;
    private void Awake()
    {
        PinBoard = GameObject.Find("PinBoard");
        GameManager = GameObject.Find("GameManager");
        
        pinBoardLogic = PinBoard.GetComponent<PinBoardLogic>();
        interact = GameManager.GetComponent<Interact>();
        CinemachineSwitcher.OnOfficeStateChanged += CinemachineSwitcher_OnOfficeStateChanged;
    }

    private void CinemachineSwitcher_OnOfficeStateChanged(OfficeState state)
    {
        pinBoardLogic.enabled = (state == OfficeState.PinBoard);
        interact.enabled = (state == OfficeState.Overview);
        
    }
}
