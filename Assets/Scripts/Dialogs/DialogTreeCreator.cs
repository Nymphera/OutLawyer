using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Linq;

public class DialogTreeCreator : MonoBehaviour
{
    
    [SerializeField]
    public Dialog dialog;
    

    [SerializeField]
    private Image crossPointPrefab, dialogOptionPrefab, linePrefab, lawyerIcon, BackGround;
    [HideInInspector]
    [SerializeField]
    private Color colorZimnaKrew, colorLuznaGadka, colorUrokOsobisty, colorProfesjonalizm, colorPodstep;
    private Canvas canvas;

    [SerializeField]
    private GameObject levelPrefab,resultPrefab,talkingImagesPrefab;

    private Transform linesParent,dialogOptionsParent,crossPointsParent,treeParent;

    float levelHeight=600;
    float levelWidth=1200;

    
    private void Awake()
    {
        BackGround = transform.GetChild(0).GetComponent<Image>();
        canvas = transform.parent.GetComponent<Canvas>();
       
        treeParent= transform;

    }
    
    public void CreateTree()
    {
        SetLevels();
       
        SetUpCrossPoints();
        SetUpDialogOptions();
        SetTalkingImages();
        SetResults();
        //¿eby rysowaæ linie potrzebujemy najpierw wszystkich pozycji
        CreateLines();

    }

    

    private void SetLevels()
    {
       int levelNum= dialog.levels.Length;
        for(int i = 0; i < levelNum; i++)
        {
            Instantiate(levelPrefab, treeParent).transform.name = "Level " + i;
        }
    }
    private void SetResults()
    {   
        int resultNum = dialog.results.Length;
        float intervalLength = levelWidth / (resultNum + 1);
        Vector3 spawnPosition;
        Transform parent = GameObject.Find("TalkingImages").transform.GetChild(0);
        for (int i = 0; i < resultNum; i++)
        {
            spawnPosition = new Vector3(-levelWidth / 2 + (i + 1) * intervalLength, -450, 0);
            GameObject obj =Instantiate(resultPrefab, parent);
            obj.transform.localPosition = spawnPosition;
            obj.transform.GetChild(2).GetComponent<Image>().sprite = dialog.results[i].resultImage;
            obj.transform.GetChild(1).GetComponent<Image>().color = dialog.results[i].ResultBarColor;
            obj.name = "Result " + i;
        }
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
                crossPointsParent = GameObject.Find("Level " + i).transform.GetChild(1);
                Image current=Instantiate(crossPointPrefab,canvas.transform.position+spawnPosition,Quaternion.Euler(0,0,45),crossPointsParent);
                current.gameObject.name = dialog.levels[i].CrossPoints[j].name;
                current.gameObject.AddComponent<CrossPointDisplay>().crossPoint= dialog.levels[i].CrossPoints[j];
                current.gameObject.GetComponent<CrossPointDisplay>().position = spawnPosition;

                if (i == 0 && j == 0)
                {
                    lawyerIcon=Instantiate(lawyerIcon, treeParent);
                    lawyerIcon.gameObject.AddComponent<DialogLawyer>().currentCrossPoint=dialog.levels[i].CrossPoints[j];
                    lawyerIcon.rectTransform.localPosition = spawnPosition;
                    lawyerIcon.name = "lawyerIcon";
                }
               
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
                dialogOptionsParent = GameObject.Find("Level " + i).transform.GetChild(2);
                spawnPosition = new Vector3(-levelWidth / 2 + (j + 1) * intervalLength, 100 + i * levelHeight, 0);
              Image currentDialogOption=  Instantiate(dialogOptionPrefab, canvas.transform.position + spawnPosition, Quaternion.identity, dialogOptionsParent);
                currentDialogOption.gameObject.name = dialog.levels[i].DialogOptions[j].name;
                
                currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().dialogOption = dialog.levels[i].DialogOptions[j];
                currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().buttonPosition = spawnPosition;
                currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().RenderImage();   //zmienia grafikê dialogoption na odpowiedni¹ strategiê
                currentDialogOption.gameObject.AddComponent<Button>().onClick.AddListener(currentDialogOption.gameObject.GetComponent<DialogOptionDisplay>().Click);
                
                  //w³¹czanie dialogu funkcji Click
                
            }
        }
    }

    private void SetTalkingImages()
    {
        GameObject obj=Instantiate(talkingImagesPrefab, treeParent.parent);
        obj.name = "TalkingImages";
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

                    linesParent = GameObject.Find("Level " + i).transform.GetChild(0);
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
                    linesParent = GameObject.Find("Level " + i).transform.GetChild(0);
                    Image image = Instantiate(linePrefab, firstPos, Quaternion.EulerRotation(new Vector3(0, 0, -rotation)),linesParent);
                    image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, lineLength);
                    image.rectTransform.localPosition = firstPos;

                image.color = SetLineColor(dialogOption.strategy);
            }
        }
    }
    private Color SetLineColor(Strategy strategy)
    {
        if (strategy == Strategy.LuŸnaGadka)
        {
            return colorLuznaGadka;
        }
        else
            if (strategy == Strategy.Podstêp)
        {
            return colorPodstep;
        }
        else
            if (strategy == Strategy.Profesjonalizm)
        {
            return colorProfesjonalizm;
        }
        else
            if (strategy == Strategy.UrokOsobisty)
        {
            return colorUrokOsobisty;
        }
        else
            if (strategy == Strategy.ZimnaKrew)
        {
            return colorZimnaKrew;
        }
        else return Color.white;
    }
    public void Click()
    {
       // lawyerIcon.rectTransform.position=GetComponent<RectTransform>().position;
    }
}
