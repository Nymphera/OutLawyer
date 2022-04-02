using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTreeCreator : MonoBehaviour
{   
    
    [SerializeField]
    private Dialog dialog;
    [SerializeField]
    private Image crossPointPrefab,dialogOptionPrefab,linePrefab,lawyerIcon,result;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    Transform treeParent;
    float treeHeight;

    float treeWidth=1000;

    private void Awake()
    {
        CreateTree();
       
    }
    public void CreateTree()
    {
        SetUpCrossPoints();
        //Instantiate(crossPointPrefab,new Vector3(0,0,0), Quaternion.identity, canvas.transform);
        
    }
    private void SetUpCrossPoints()
    {
        int dialoglevelsNum= dialog.levels.Length;
        int crosspointNum;
        float intervalLength;
        float intervalHeight=300;
        for(int i = 0; i < dialoglevelsNum; i++)   //pêtla przez wszystkie levele
        {
            crosspointNum = dialog.levels[i].CrossPoints.Length;
            Vector3 spawnPosition;
            intervalLength = treeWidth / (crosspointNum + 1);   // d³ugoœæ interwa³u pomiêdzy lewym i prawym bokiem drzewka
            for(int j = 0; j < crosspointNum; j++)
            {

                 spawnPosition = new Vector3 (-500 +(j+1)*intervalLength, -200 +i*intervalHeight, 0);
                Instantiate(crossPointPrefab,canvas.transform.position+spawnPosition,Quaternion.Euler(0,0,45),treeParent);
            }
        }

    }
}
