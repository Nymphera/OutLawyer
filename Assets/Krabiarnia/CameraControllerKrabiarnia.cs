using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerKrabiarnia : MonoBehaviour
{
    Animator animator;
    public static CameraControllerKrabiarnia Instance;
    [SerializeField]
    GameObject mouse;
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        mouse=GameObject.Find("Mouse");
    }
    public void SwitchState(string animationName)
    {
        if (animationName == "Player")
        {
            animator.Play(animationName);
            GameManager.Instance.UpdateGameState(GameState.Move);
        }
        else if (animationName == "Negotiations")
        {
            animator.Play(animationName);
            GameManager.Instance.UpdateGameState(GameState.LockInteract);
        }
        else if (animationName == "DialogWithKrabiarz")
        {
            animator.Play(animationName);
            GameManager.Instance.UpdateGameState(GameState.LockInteract);
        }
        else if (animationName == "DialogWithAldhest")
        {
            animator.Play(animationName);
            GameManager.Instance.UpdateGameState(GameState.LockInteract);
        }
        else if (animationName == "DialogWithHodowca")
        {
            animator.Play(animationName);
            GameManager.Instance.UpdateGameState(GameState.LockInteract);
        }

       // mouse.SetActive(GameManager.Instance.CurrentState != GameState.Interact);
    }
}
