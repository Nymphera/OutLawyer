using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTreeCreator : MonoBehaviour
{   
    
    [SerializeField]
    private Dialog dialog;
    [SerializeField]
    private Image crossPointPrefab, dialogOptionPrefab, linePrefab, lawyerIcon, result, BackGround;
    [SerializeField]
    Sprite strategy1, strategy2, strategy3, strategy4, strategy5;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    Transform treeParent;

    float levelHeight=600;
    float levelWidth=1400;

    private void Awake()
    {
        BackGround.rectTransform.sizeDelta = new Vector2(levelWidth,levelHeight*dialog.levels.Length);
        CreateTree();
       
    }
    public void CreateTree()
    {
        SetUpCrossPoints();
        SetUpDialogOptions();
        //Instantiate(crossPointPrefab,new Vector3(0,0,0), Quaternion.identity, canvas.transform);
        
    }
    private void SetUpCrossPoints()
    {
        int dialoglevelsNum= dialog.levels.Length;
        int crosspointNum;
        float intervalLength;
        
        for(int i = 0; i < dialoglevelsNum; i++)   //pêtla przez wszystkie levele
        {
            crosspointNum = dialog.levels[i].CrossPoints.Length;
            Vector3 spawnPosition;
            intervalLength = levelWidth / (crosspointNum + 1);   // d³ugoœæ interwa³u pomiêdzy lewym i prawym bokiem drzewka
            for(int j = 0; j < crosspointNum; j++)
            {

                 spawnPosition = new Vector3 (-levelWidth/2 +(j+1)*intervalLength, -200 +i*levelHeight, 0);
                Instantiate(crossPointPrefab,canvas.transform.position+spawnPosition,Quaternion.Euler(0,0,45),treeParent);
            }
        }

    }
    private void SetUpDialogOptions()
    {
        int dialoglevelsNum = dialog.levels.Length;
        int dialogOptionNum;
        float intervalLength;
        for (int i = 0; i < dialoglevelsNum; i++)   //pêtla przez wszystkie levele
        {
            dialogOptionNum = dialog.levels[i].DialogOptions.Length;
            Vector3 spawnPosition;
            intervalLength = levelWidth / (dialogOptionNum + 1);   // d³ugoœæ interwa³u pomiêdzy lewym i prawym bokiem drzewka
            for (int j = 0; j < dialogOptionNum; j++)
            {

                spawnPosition = new Vector3(-levelWidth / 2 + (j + 1) * intervalLength, 100 + i * levelHeight, 0);
              Image currentDialogOption=  Instantiate(dialogOptionPrefab, canvas.transform.position + spawnPosition, Quaternion.identity, treeParent);

                //dodawanie odpowiedniego zdjêcia strategii
                if (dialog.levels[i].DialogOptions[j].strategy == Strategy.ZimnaKrew)
                {
                    currentDialogOption.sprite = strategy1;
                }
                else
                     if (dialog.levels[i].DialogOptions[j].strategy == Strategy.Podstêp)
                {
                    currentDialogOption.sprite = strategy2;
                }
                else
                     if (dialog.levels[i].DialogOptions[j].strategy == Strategy.Profesjonalizm)
                {
                    currentDialogOption.sprite = strategy3;
                }
                else
                     if (dialog.levels[i].DialogOptions[j].strategy == Strategy.LuŸnaGadka)
                {
                    currentDialogOption.sprite = strategy4;
                }
                else
                     if (dialog.levels[i].DialogOptions[j].strategy == Strategy.UrokOsobisty)
                {
                    currentDialogOption.sprite = strategy5;
                }
                

            }
        }
    }

    
}
