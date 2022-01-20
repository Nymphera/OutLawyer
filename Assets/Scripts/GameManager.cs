using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Evidence;
    private GameObject PinBoard;
    private bool ActiveStatus = false;
   
    private void Start()
    {
        PinBoard = GameObject.Find("PinBoard");
        PinBoard.SetActive(true);
       
    }


    // Update is called once per frame
    void Update()
    {

        //Operacja otwierania i zamykania tablicy korkowej
       
        if (Input.GetKeyUp(KeyCode.K))
        {   if (ActiveStatus == false)
               ActiveStatus= PinBoardOpening();
            else if (ActiveStatus == true)
               ActiveStatus= PinBoardClosing();
        }
      
        
 
        


    }
     bool PinBoardOpening()
    {
        PinBoard.SetActive(true);
        return true;
    }
    bool PinBoardClosing()
    {      
        PinBoard.SetActive(false);
        return false;
    }



}

