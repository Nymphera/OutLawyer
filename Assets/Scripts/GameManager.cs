using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject PinBoard;
   
   
    private void Start()
    {
        PinBoard = GameObject.Find("Tablica Korkowa");
        PinBoard.SetActive(false);
       
    }


    // Update is called once per frame
    void Update()
    {
      
        //Operacja otwierania i zamykania tablicy korkowej

        if (Input.GetKeyDown(KeyCode.K))
        {
            PinBoardOpening();
        }
        //Nie wiem jak zrobiæ, ¿eby Tablica otwiera³a siê i zamyka³a na jednym przycisku. 
        //W tym momencie jeœli tak zrobiê w czasie jednego klikniêcia Tablica otwiera siê i od razu zamyka
        //Wiêc na razie zostawiam "Escape"

        if (Input.GetKeyDown(KeyCode.Escape)&& PinBoard.activeSelf==true)
        {
            PinBoardClosing();
        }


    }
     void PinBoardOpening()
    {
        PinBoard.SetActive(true);
    }
    private void PinBoardClosing()
    {      
        PinBoard.SetActive(false);
    }



}

