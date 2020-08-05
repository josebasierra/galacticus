using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableAudio 
{
    public struct TimeCut
    {
        public bool isPlaying;
        public AudioSource source;
        public int timeSamples;

        public TimeCut(bool _isPlaying, AudioSource _source, int _timeSamples)
        {
            isPlaying = _isPlaying;
            source = _source;
            timeSamples = _timeSamples;
        }
    }

    Rewindable rewindable;
    AudioComponent audioComponent;

    LimitedStack<TimeCut[]> audioRegister;

    public RewindableAudio(Rewindable _rewindable, AudioComponent _audioComponent)
    {
        rewindable = _rewindable;
        audioComponent = _audioComponent;

        audioRegister = new LimitedStack<TimeCut[]>(rewindable.MaxCapacity());

        rewindable.OnRewind += OnRewind;
        rewindable.OnRecord += OnRecord;
    }


    ~RewindableAudio()
    {
        rewindable.OnRewind -= OnRewind;
        rewindable.OnRecord -= OnRecord;
    }


    void OnRewind(float time)
    {
        var timeCuts = audioRegister.Top();
        foreach (var timeCut in timeCuts)
        {
            if (timeCut.isPlaying)
            {
                timeCut.source.volume = audioComponent.DefaultVolume(timeCut.source)*0.9f;
                timeCut.source.pitch = -1;
                timeCut.source.timeSamples = timeCut.timeSamples;

                if (!timeCut.source.isPlaying) timeCut.source.Play();
            }  
        }
        audioRegister.Pop();
    }


    void OnRecord()
    {
        var sources = audioComponent.Sources();
        foreach (var source in sources)
        {
            audioComponent.RestoreDefaultSettings(source);
        }


        var timeCuts = new TimeCut[sources.Length];
        for (int i = 0; i < timeCuts.Length; i++)
        {
            timeCuts[i].isPlaying = sources[i].isPlaying;
            timeCuts[i].source = sources[i];
            timeCuts[i].timeSamples = sources[i].timeSamples;
        }

        audioRegister.Push(timeCuts);
    }
}
