using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Transform mainMenuCanvas;
    public Transform hireFireCanvas;
    //private Transform saveTransform;
    //private Transform loadTransform;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) mainMenuCanvas.gameObject.SetActive(!mainMenuCanvas.gameObject.activeSelf);//closing and opening the menu
        if (Input.GetKeyDown(KeyCode.F)) hireFireCanvas.gameObject.SetActive(!hireFireCanvas.gameObject.activeSelf);
    }
}
