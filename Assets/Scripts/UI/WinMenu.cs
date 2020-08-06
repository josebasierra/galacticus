using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WinMenu : MonoBehaviour
{
    [SerializeField] GameObject canvasObject;


    void Start()
    {
        canvasObject.SetActive(false);
        GameManager.Instance().OnWin += OnWin;
    }


    private void OnDestroy()
    {
        GameManager.Instance().OnWin -= OnWin;
    }


    void OnWin()
    {
        canvasObject.SetActive(true);
        GameManager.Instance().Pause();
    }
}

