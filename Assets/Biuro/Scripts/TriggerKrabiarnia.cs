using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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
        await Task.Delay(10);
        GameManager.Instance.UpdateGameState(GameState.Move);
    }
}
