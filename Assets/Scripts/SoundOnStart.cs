using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnStart : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    void Start()
    {
        var audioComponent = GetComponent<AudioComponent>();
        if (audioComponent != null && clip != null)
        {
            audioComponent.Play(clip);
        }
    }


}
