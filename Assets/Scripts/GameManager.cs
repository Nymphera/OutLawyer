using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Evidence;
    private GameObject PinBoard;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged; 
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PinBoard = GameObject.Find("PinBoard");
        PinBoard.SetActive(false);
        UpdateGameState(GameState.PlayerMove);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.PlayerMove:
                break;
            case GameState.PinBoard:

                break;
        }
        OnGameStateChanged(newState);
    }
        public void OpenPinBoard(bool isOpen)
        {
            if(isOpen==false)
            {
             PinBoard.SetActive(true);

            }
            else //isOpen==true
            {
                PinBoard.SetActive(false);
            }
        }

    }
    public enum GameState
    {
        PlayerMove,
        PinBoard
    }
