using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseView;
    [SerializeField] GameObject controlsView;

    GameObject currentView;

    bool canPause = true;
    bool isPaused = false;


    public void Continue()
    {
        isPaused = false;
        GameManager.Instance().Continue();
        currentView.SetActive(false);
    }

    public void ShowPauseView()
    {
        ShowView(pauseView);
    }


    public void ShowControlsView()
    {
        ShowView(controlsView);
    }


    void Start()
    {
        pauseView.SetActive(false);
        controlsView.SetActive(false);

        GameManager.Instance().OnWin += OnWin;
    }


    void ShowView(GameObject view)
    {
        if (currentView != null) currentView.SetActive(false);
        currentView = view;
        currentView.SetActive(true);
    }


    void OnCancelView()
    {
        if (!isPaused && canPause)
        {
            isPaused = true;
            GameManager.Instance().Pause();
            ShowPauseView();
        }
        else if (isPaused && currentView == pauseView)
        {
            Continue();
        }
        else if (isPaused && currentView == controlsView)
        {
            ShowPauseView();
        }
    }


    void OnWin()
    {
        canPause = false;
    }


    void OnDestroy()
    {
        GameManager.Instance().OnWin -= OnWin;
    }



}
