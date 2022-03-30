using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DialogTreeCreator : MonoBehaviour
{
    
    [SerializeField]
    private Dialog dialog;
    [SerializeField]
    private Image crossPointPrefab, dialogOptionPrefab, linePrefab, lawyerIcon, result, BackGround,checkPosition;
   
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    Transform linesParent,dialogOptionsParent,crossPointsParent,treeParent;

    float levelHeight=600;
    float levelWidth=1200;

    
    private void Awake()
    {
        BackGround.rectTransform.sizeDelta = new Vector2(levelWidth,levelHeight*dialog.levels.Length);
        CreateTree();
       
    }
    public void CreateTree()
    {
        
        SetUpCrossPoints();
        SetUpDialogOptions();   //�eby rysowa� linie potrzebujemy najpierw wszystkich pozycji
        CreateLines();

    }
    private void SetUpCrossPoints()
    {
        int dialoglevelsNum= dialog.levels.Length;
        int crosspointNum;
        float intervalLength;
        int index = 0;
        for(int i = 0; i < dialoglevelsNum; i++)   //p�tla przez wszystkie levele
        {
            crosspointNum = dialog.levels[i].CrossPoints.Length;
            Vector3 spawnPosition;
            intervalLength = levelWidth / (crosspointNum + 1);   // d�ugo�� interwa�u pomi�dzy lewym i prawym bokiem drzewka
            for(int j = 0; j < crosspointNum; j++)
            {
                index++;
                 spawnPosition = new Vector3 (-levelWidth/2 +(j+1)*intervalLength, -200 +i*levelHeight, 0);
                Image current=Instantiate(crossPointPrefab,canvas.transform.position+spawnPosition,Quaternion.Euler(0,0,45),crossPointsParent);
                current.gameObject.name = dialog.levels[i].CrossPoints[j].name;
                current.gameObject.AddComponent<CrossPointDisplay>().crossPoint= dialog.levels[i].CrossPoints[j];
                current.gameObject.GetComponent<CrossPointDisplay>().position = spawnPosition;

                if (i == 0 && j == 0)
                {
                    lawyerIcon=Instantiate(lawyerIcon, treeParent);
                    lawyerIcon.rectTransform.localPosition = spawnPosition;
                    
                }
               
            }
        }

    }
    private void SetUpDialogOptions()
    {
        int dialoglevelsNum = dialog.levels.Length;
        int dialogOptionNum;
        float intervalLength;
        for (int i = 0; i < dialoglevelsNum; i++)   //p�tla przez wszystkie levele
        {
            dialogOptionNum = dialog.levels[i].DialogOptions.Length;
            Vector3 spawnPosition;
            intervalLength = levelWidth / (dialogOptionNum + 1);   // d�ugo�� interwa�u pomi�dzy lewym i prawym bokiem drzewka
            for (int j = 0; j < dialogOptionNum; j++)
            {

                spawnPosition = new Vector3(-levelWidth / 2 + (j + 1) * intervalLength, 100 + i * levelHeight, 0);
              Image currentDialogOption=  Instantiate(dialogOptionPrefab, canvas.transform.position + spawnPosition, Quaternion.identity, dialogOptionsParent);
                currentDialogOption.gameObject.name = dialog.levels[i].DialogOptions[j].name;
                currentDialogOption.gameObject.AddComponent<DialogOptionDisplay>().dialogOption = dialog.levels[i].DialogOptions[j];
                currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().buttonPosition = spawnPosition;
                currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().RenderImage();   //zmienia grafik� dialogoption na odpowiedni� strategi�
                currentDialogOption.gameObject.AddComponent<Button>().onClick.AddListener(currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().Click);
                
                  //w��czanie dialogu funkcji Click
                
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

                    

                    GameObject temp = GameObject.Find(crossPoint.name);
                    GameObject temp2 = GameObject.Find(dialogOption.name);
                    Vector2 firstPos = temp.GetComponent<CrossPointDisplay>().position;
                    
                    Vector2 secondPos = temp2.GetComponent<DialogOptionDisplay>().buttonPosition;

                    
                    float lineLength = Vector2.Distance(firstPos, secondPos);

                  float rotation=Mathf.Atan2(secondPos.x-firstPos.x,secondPos.y-firstPos.y);    //rotacja w radianach
                    Image image=Instantiate(linePrefab,firstPos, Quaternion.EulerRotation(new Vector3(0, 0, -rotation)), linesParent);
                    image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, lineLength);
                    image.rectTransform.localPosition = firstPos;
                    image.color = SetLineColor(dialogOption.strategy);
                }
            }
        }

        for (int i = 0; i < dialog.levels.Length; i++)
        {
            Level level = dialog.levels[i];

            for (int j = 0; j < level.DialogOptions.Length; j++)
            {
               
                DialogOption dialogOption = level.DialogOptions[j];
                CrossPoint crossPoint = dialogOption.nextCrossPoint;

                GameObject temp = GameObject.Find(dialogOption.name);
                GameObject temp2 = GameObject.Find(crossPoint.name);
                    
                    Vector2 firstPos = temp.GetComponent<DialogOptionDisplay>().buttonPosition;

                    Vector2 secondPos = temp2.GetComponent<CrossPointDisplay>().position;

                    float lineLength = Vector2.Distance(firstPos, secondPos);

                    float rotation = Mathf.Atan2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);    //rotacja w radianach

                    Image image = Instantiate(linePrefab, firstPos, Quaternion.EulerRotation(new Vector3(0, 0, -rotation)),linesParent);
                    image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, lineLength);
                    image.rectTransform.localPosition = firstPos;

                image.color = SetLineColor(dialogOption.strategy);
            }
        }
    }
    private Color SetLineColor(Strategy strategy)
    {
        if (strategy == Strategy.Lu�naGadka)
        {
            return Color.blue;
        }
        else
            if (strategy == Strategy.Podst�p)
        {
            return Color.magenta;
        }
        else
            if (strategy == Strategy.Profesjonalizm)
        {
            return Color.yellow; //co to kurwa za brak fajnych kolork�w
        }
        else
            if (strategy == Strategy.UrokOsobisty)
        {
            return Color.green;
        }
        else
            if (strategy == Strategy.ZimnaKrew)
        {
            return Color.red;
        }
        else return Color.white;
    }
    public void Click()
    {
        lawyerIcon.rectTransform.position=GetComponent<RectTransform>().position;
    }
}
