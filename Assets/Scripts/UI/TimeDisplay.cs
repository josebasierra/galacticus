using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{

    TextMeshProUGUI textMesh;


    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }


    void FixedUpdate()
    {
        float time = GameManager.Instance().GetCurrentLevelTime();
        float time_ms = time * 1000;

        int ms = ((int)time_ms % 1000)/100;
        int s = (int)(time_ms / 1000) % 60;
        int min = (int)(time_ms / 1000) / 60;


        textMesh.text = min.ToString("00") + ":" + s.ToString("00") + "." + ms.ToString("0");
    }
}
