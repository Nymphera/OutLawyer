using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToOffice : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OfficeManager.Instance.UpdateOfficeState(OfficeState.Overview);
        GameManager.Instance.UpdateGameState(GameState.Office);
    }
}
