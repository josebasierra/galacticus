using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainView;
    [SerializeField] GameObject controlsView;

    GameObject currentView;


    public void ShowMainView()
    {
        ShowView(mainView);
    }


    public void ShowControlsView()
    {
        ShowView(controlsView);
    }


    void Start()
    {
        mainView.SetActive(false);
        controlsView.SetActive(false);
        ShowMainView();
    }


    void ShowView(GameObject view)
    {
        if (currentView != null) currentView.SetActive(false);
        currentView = view;
        currentView.SetActive(true);
    }


    void OnCancelView()
    {
        if (currentView == controlsView)
            ShowMainView();
    }


}
