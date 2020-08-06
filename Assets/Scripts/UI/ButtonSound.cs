using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayButtonSound);
    }

    void PlayButtonSound()
    {
        AudioManager.Instance().PlayShot(clip);
    }
}
