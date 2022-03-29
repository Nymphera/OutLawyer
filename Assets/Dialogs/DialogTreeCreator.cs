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
        SetUpDialogOptions();   //¿eby rysowaæ linie potrzebujemy najpierw wszystkich pozycji
        CreateLines();

    }
    private void SetUpCrossPoints()
    {
        int dialoglevelsNum= dialog.levels.Length;
        int crosspointNum;
        float intervalLength;
        int index = 0;
        for(int i = 0; i < dialoglevelsNum; i++)   //pêtla przez wszystkie levele
        {
            crosspointNum = dialog.levels[i].CrossPoints.Length;
            Vector3 spawnPosition;
            intervalLength = levelWidth / (crosspointNum + 1);   // d³ugoœæ interwa³u pomiêdzy lewym i prawym bokiem drzewka
            for(int j = 0; j < crosspointNum; j++)
            {
                index++;
                 spawnPosition = new Vector3 (-levelWidth/2 +(j+1)*intervalLength, -200 +i*levelHeight, 0);
                Image current=Instantiate(crossPointPrefab,canvas.transform.position+spawnPosition,Quaternion.Euler(0,0,45),treeParent);
                current.gameObject.name = dialog.levels[i].CrossPoints[j].name;
                current.gameObject.AddComponent<CrossPointDisplay>().crossPoint= dialog.levels[i].CrossPoints[j];
                current.gameObject.GetComponent<CrossPointDisplay>().position = spawnPosition;

                int conectedDialogOptionsNum= dialog.levels[i].CrossPoints[j].ConectedDialogOptions.Length;
               
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
                currentDialogOption.gameObject.name = dialog.levels[i].DialogOptions[j].name;
                currentDialogOption.gameObject.AddComponent<DialogOptionDisplay>().dialogOption = dialog.levels[i].DialogOptions[j];
                currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().position = spawnPosition;
                currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().RenderImage();   //zmienia grafikê dialogoption na odpowiedni¹ strategiê

               



            }
        }
    }
    private void CreateLines()
    {
        for(int i = 0; i < dialog.levels.Length;i++)
        {
           Level level= dialog.levels[i];

            for(int j = 0; j < level.CrossPoints.Length; j++)
            {
                CrossPoint crossPoint = level.CrossPoints[j];

                for (int k = 0; k < crossPoint.ConectedDialogOptions.Length; k++)
                {
                    DialogOption dialogOption = crossPoint.ConectedDialogOptions[k];

                    //¿adnych ifów !!!

                    GameObject temp = GameObject.Find(crossPoint.name);
                    GameObject temp2 = GameObject.Find(dialogOption.name);
                    Vector2 firstPos = temp.transform.position;
                    Vector2 secondPos = temp2.transform.position;
                    float lineLength = Vector2.Distance(firstPos, secondPos);

                  float rotation=Mathf.Atan2(secondPos.x-firstPos.x,secondPos.y-firstPos.y);    //rotacja w radianach
                    Image image=Instantiate(linePrefab,firstPos,Quaternion.identity,treeParent);
                    image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, lineLength);
                    image.rectTransform.rotation = Quaternion.EulerRotation(new Vector3(0,0,rotation));
                }
            }
        }
    }

    
}
