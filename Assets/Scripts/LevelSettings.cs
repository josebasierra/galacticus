using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] AudioClip music;

    void Start()
    {
        if (music != null) AudioManager.Instance().PlayMusic(music);
    }
}
