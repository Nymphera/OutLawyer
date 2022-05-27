using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerKrabiarnia : MonoBehaviour
{
    private void OnMouseDown()
    {
        LoadKrabiarnia();
    }
    public void LoadKrabiarnia()
    {
        GameEvents.current.OfficeClick(0);
        SceneManager.LoadScene("Krabiarnia");
        GameManager.Instance.UpdateGameState(GameState.Move);
    }
}
