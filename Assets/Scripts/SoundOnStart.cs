using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnStart : MonoBehaviour
{

    [SerializeField] AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        var audioComponent = GetComponent<AudioComponent>();
        if (audioComponent != null && clip != null)
        {
            audioComponent.Play(clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
