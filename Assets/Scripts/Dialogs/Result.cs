using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public Strategy strategy1, strategy2;
    [SerializeField]
    public GameObject ResultBar;
    private void Awake()
    {
        ResultBar = this.transform.GetChild(1).gameObject;
    }
}
