using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Evidence;
    private GameObject PinBoard;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PinBoard = GameObject.Find("PinBoard");
        PinBoard.SetActive(false);
       
    }


    // Update is called once per frame
    void Update()
    {

       
       
     
      
        
 
        


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





