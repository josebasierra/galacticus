using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioComponent : MonoBehaviour
{
    Dictionary<AudioClip, AudioSource> clipToSource;
    Dictionary<AudioSource, DefaultSettings> sourceToDefault;

    public struct DefaultSettings
    {
        public float volume;
        public float pitch;

        public DefaultSettings(float _volume, float _pitch)
        {
            volume = _volume;
            pitch = _pitch;
        }
    }


    void Start()
    {
        clipToSource = new Dictionary<AudioClip, AudioSource>();
        sourceToDefault = new Dictionary<AudioSource, DefaultSettings>();
    }


    public void Play(AudioClip clip)
    {
        if (!clipToSource.TryGetValue(clip, out AudioSource source))
        {
            source = gameObject.AddComponent<AudioSource>();
            clipToSource.Add(clip, source);
            AddDefaultSettings(source);
        }
        
        source.clip = clip;
        source.Play(); 
    }


    public AudioSource Source(AudioClip clip)
    {
        return clipToSource[clip];
    }


    public AudioSource[] Sources()
    {
        var sourcesCollection = clipToSource.Values;

        var sources = new AudioSource[sourcesCollection.Count];
        sourcesCollection.CopyTo(sources, 0);

        return sources;
    }


    public void RestoreDefaultSettings(AudioSource source)
    {
        var defaultSettings = sourceToDefault[source];
        source.volume = defaultSettings.volume;
        source.pitch = defaultSettings.pitch;
    }


    public float DefaultVolume(AudioSource source)
    {
        return sourceToDefault[source].volume;
    }


    void AddDefaultSettings(AudioSource source)
    {
        sourceToDefault[source] = new DefaultSettings(source.volume, source.pitch);
    }
}
