using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    Transform mainCamera;
    bool musicStopped = false;

    static AudioManager instance;
    public static AudioManager Instance()
    {
        return instance;
    }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown("m"))
    //    {
    //        if (!musicStopped) 
    //        {
    //            musicStopped = true;
    //            audioSource.Stop();
    //        }
    //        else
    //        {
    //            musicStopped = false;
    //            audioSource.Play();
    //        }
    //    }
        
    //}

    void FixedUpdate()
    {
        transform.position = mainCamera.position;
    }


    public static void PlayShot(AudioSource source, AudioClip clip)
    {
        if (source == null)
        {
            Debug.Log("Missing audio source");
            return;
        }
        if (clip == null)
        {
            Debug.Log("Missing audio clip");
            return;
        }

        source.PlayOneShot(clip);
    }


    public void PlayShot(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("Missing audio clip");
        }
        audioSource.PlayOneShot(clip);
    }


    public void PlayMusic(AudioClip music)
    {
        if (music == null)
        {
            Debug.Log("Missing music clip");
            return;
        }
        if (music == audioSource.clip)
        {
            return;
        }

        audioSource.clip = music;
        audioSource.Play();
    }


    public void SetPitch(float value)
    {
        audioSource.pitch = value;
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main.transform;
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnMuteMusic()
    {
        Debug.Log("Music turn on/off");
        audioSource.volume = (audioSource.volume + 1) % 2;
    }
}
