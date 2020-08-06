using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatMenu : MonoBehaviour
{
    [SerializeField] GameObject canvasObject;


    void Start()
    {
        canvasObject.SetActive(false);
        GameManager.Instance().OnDefeat += OnDefeat;
    }

    void OnDestroy()
    {
        GameManager.Instance().OnDefeat -= OnDefeat;
    }

    void OnDefeat()
    {
        canvasObject.SetActive(true);
    }
}
