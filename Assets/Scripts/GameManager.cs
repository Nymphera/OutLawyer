using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private GameObject PinBoard;
    void Start()
    {
        PinBoard = GameObject.Find("Tablica Korkowa");
        PinBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PinBoardOpening();
    }
    void PinBoardOpening()
    {
        //bool ActiveStatus=false;
        if (Input.GetKey(KeyCode.K))
        {
            
            PinBoard.SetActive(true);
           // ActiveStatus = true;
            
        }
        

    }
}
