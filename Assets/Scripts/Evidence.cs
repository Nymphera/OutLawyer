using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "New Evidence", menuName = "Evidences", order = 1)]
    public class Evidence : ScriptableObject
    {
    public LayerMask PinBoard;
        public string EvidenceName;
        public string Description;
        public Material Artwork;
    public GameObject Object;
    private Camera cam;
    private Material Material;


    public void ShowDescription()
    {
        Debug.Log(Description);
    }
    public void DragPicture()
    {
        Object.transform.position = Input.mousePosition;
    }
    private void GetBoundaries(GameObject Object)
    {
        Vector3 Boundaries = Object.GetComponent<Transform>().localScale;
    }
    }

