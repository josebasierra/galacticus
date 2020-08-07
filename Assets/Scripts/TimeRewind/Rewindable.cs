using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rewindable : MonoBehaviour
{
    [SerializeField] float maximumRewindSeconds = 10f;

    public event Action<float> OnRewind;
    public event Action OnRecord;

    public static readonly float MIN_REWIND_TIME = 0.10f;
    static List<Rewindable> instances;

    [HideInInspector] public RewindableDestruction rewindableDestruction;

    RewindableRigidbody rewindableRigidbody;
    RewindableHealth rewindableHealth;
    RewindablePartSystem rewindableParticleSystem;
    RewindableAudio rewindableAudio;

    Highlighter highlighter;

    LimitedStack<float> timeRegister;

    bool isRewinding = false;
    float secondsToBeRewinded = 0;
    float currentRewindedSeconds = 0;
    float recordedSeconds = 0;


    void Start()
    {
        highlighter = gameObject.AddComponent<Highlighter>();
        rewindableDestruction = gameObject.AddComponent<RewindableDestruction>();

        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rewindableRigidbody = new RewindableRigidbody(this, transform, rigidbody);
        }

        var health = GetComponent<Health>();
        if (health != null)
        {
            rewindableHealth = new RewindableHealth(this, health);
        }

        var pSystem = GetComponent<ParticleSystem>();
        if (pSystem != null)
        {
            rewindableParticleSystem = new RewindablePartSystem(this, pSystem);
        }

        var audioComponent = GetComponent<AudioComponent>();
        if (audioComponent != null)
        {
            rewindableAudio = new RewindableAudio(this, audioComponent);
        }

        timeRegister = new LimitedStack<float>(MaxCapacity());

        if (instances == null)
        {
            instances = new List<Rewindable>();
        }
        instances.Add(this);


        Record();
    }


    void OnDestroy()
    {
        instances.Remove(this);
    }


    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }


    public static List<Rewindable> GetInstances()
    {
        return instances;
    }

    public int MaxCapacity()
    {
        return (int)(maximumRewindSeconds / Time.fixedDeltaTime);
    }

    public bool IsAtMaxCapacity()
    {
        return timeRegister.Count() >= timeRegister.MaxCapacity();
    }


    public bool IsDestroyed()
    {
        return rewindableDestruction.IsDestroyed();
    }


    public bool IsRewinding()
    {
        return isRewinding;
    }


    public float GetMaximumRewindSeconds()
    {
        return maximumRewindSeconds;
    }


    public void StartRewind(float time, bool highlight = false)
    {
        if (highlight) highlighter?.HighlightOn();
        else highlighter?.HighlightOff();

        isRewinding = true;
        secondsToBeRewinded = Mathf.Max(MIN_REWIND_TIME, time);
        currentRewindedSeconds = 0;
    }


    public void StopRewind()
    {
        highlighter?.HighlightOff();

        isRewinding = false;
    }


    void Rewind()
    {
        if (timeRegister.IsEmpty())
        {
            StopRewind();
            Record();
            
            if (recordedSeconds < maximumRewindSeconds)
            {
                Destroy(this.gameObject);
            }
            return;
        }
        //if (timeRegister.IsEmpty() && recordedSeconds < maximumRewindSeconds)         // if reached origin/instantiation of object
        //{
        //    Debug.Log("Rewindable: " + Time.fixedTime.ToString());
        //    Destroy(this.gameObject);
        //}

        OnRewind?.Invoke(timeRegister.Top());
        timeRegister.Pop();

        currentRewindedSeconds += Time.fixedDeltaTime;


        //if this was the last rewind:

        //if (timeRegister.IsEmpty() && recordedSeconds < maximumRewindSeconds)         // if reached origin/instantiation of object
        //{
        //    Debug.Log("Rewindable: " + Time.fixedTime.ToString());
        //    Destroy(this.gameObject);
        //}
        if (!timeRegister.IsEmpty() && currentRewindedSeconds > secondsToBeRewinded)
        {
            StopRewind();
            Record();
        }
    }


    void Record()
    {
        OnRecord?.Invoke();
        timeRegister.Push(Time.fixedTime);
        recordedSeconds += Time.fixedDeltaTime;
    }
}



