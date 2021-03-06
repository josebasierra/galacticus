﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//SINGLETON CLASS	
public class GameManager : MonoBehaviour
{
    [SerializeField] Material defaultHighlightMaterial;

    public event Action OnWin, OnDefeat;
    static GameManager instance;

    bool isPaused = false;
    float currentLevelTime = 0f;


    public static GameManager Instance()
    {
        return instance;
    }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            CustomDestroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    void FixedUpdate()
    {
        if (!isPaused)
        {
            currentLevelTime += Time.fixedDeltaTime;
        }
    }


    public Material GetHighlightMaterial()
    {
        return defaultHighlightMaterial;
    }


    public float GetCurrentLevelTime()
    {
        return currentLevelTime;
    }


    public void SetCurrentLevelTime(float value)
    {
        currentLevelTime = value;
    }


    public void CustomDestroy(GameObject objectToDestroy)
    {
        var rDestruction = objectToDestroy.GetComponent<RewindableDestruction>();
        if (rDestruction == null)
        {
            Destroy(objectToDestroy);
        }
        else
        {
            rDestruction.RewindableDestroy();
        }
    }

    // game/menu state

    public void Win()
    {
        OnWin?.Invoke();
        Stop();
        Debug.Log("Level completed in " + currentLevelTime.ToString());
    }

    public void Defeat()
    {
        OnDefeat?.Invoke();
        Stop();
        Debug.Log("Game over");
    }

    public void Stop()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        isPaused = false;
        Time.timeScale = 1;
    }


    // Scene management

    public void LoadScene(string sceneName)
    {
        Continue();
        currentLevelTime = 0;
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();   
    }
}
