using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Functions : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance().ReloadScene();
    }

    public void LoadScene(string sceneName)
    {
        GameManager.Instance().LoadScene(sceneName);
    }

    public void QuitGame()
    {
        GameManager.Instance().QuitGame();
    }

    public void Activate(GameObject objectToActivate)
    {
        objectToActivate.SetActive(true);
    }


    public void Desactivate(GameObject objectToDesactivate)
    {
        objectToDesactivate.SetActive(false);
    }
}
