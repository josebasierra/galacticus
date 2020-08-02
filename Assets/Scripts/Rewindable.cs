using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rewindable : MonoBehaviour
{
    public struct TimeCut
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;

        public float time;

        public TimeCut(Transform transform, Rigidbody rigidbody, float time)
        {
            position = transform.position;
            rotation = transform.rotation;
            velocity = rigidbody.velocity;
            angularVelocity = rigidbody.angularVelocity;
            this.time = time;
        }
    }

    [SerializeField] float maximumRewindSeconds = 5f;

    public event Action<float> OnRewind;
    public event Action OnRecord;

    static List<Rewindable> instances;

    Rigidbody myRigidbody;
    Highlighter highlighter;
    List<TimeCut> timeCuts;

    bool isRewinding = false;
    float secondsToBeRewinded = 0;
    float currentRewindedSeconds = 0;
    float recordedSeconds = 0;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        highlighter = gameObject.AddComponent<Highlighter>();
        gameObject.AddComponent<RewindableDestruction>();
        timeCuts = new List<TimeCut>();
    }


    void OnEnable()
    {
        if (instances == null)
        {
            instances = new List<Rewindable>();
        }
        instances.Add(this);
    }


    void OnDisable()
    {
        instances.Remove(this);
    }


    //TODO: Fix error after many continued rewinds (teleports, missing positions/timeCuts ?)
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


    public bool IsTimeRecordingFull()
    {
        int maximumTimeCuts = (int)(maximumRewindSeconds / Time.fixedDeltaTime);
        return timeCuts.Count > maximumTimeCuts;
    }


    public bool IsRewinding()
    {
        return isRewinding;
    }


    public float GetMaximumRewindSeconds()
    {
        return maximumRewindSeconds;
    }


    public void StartRewind(float time)
    {
        highlighter.HighlightOn();

        isRewinding = true;
        secondsToBeRewinded = Mathf.Max(Time.fixedDeltaTime, time);
        currentRewindedSeconds = 0;
    }


    public void StopRewind()
    {
        highlighter.HighlightOff();

        isRewinding = false;
    }


    void Rewind()
    {
        if (currentRewindedSeconds > secondsToBeRewinded)
        {
            StopRewind();
            Record();
            return;
        }
        else if (timeCuts.Count <= 0)
        {
            StopRewind();
            if (recordedSeconds < maximumRewindSeconds)         // if reached origin/instantiation of object
            {
                Destroy(this.gameObject);
            }
            return;
        }
        
        var timeCut = timeCuts[timeCuts.Count - 1];
        ApplyState(timeCut);

        OnRewind?.Invoke(timeCut.time);

        timeCuts.RemoveAt(timeCuts.Count - 1);
        currentRewindedSeconds += Time.fixedDeltaTime;
    }


    void Record()
    {
        if (IsTimeRecordingFull())
        {
            timeCuts.RemoveAt(0);
        }

        OnRecord?.Invoke();

        timeCuts.Add(new TimeCut(transform, myRigidbody, Time.fixedTime));

        recordedSeconds += Time.fixedDeltaTime;
    }


    void ApplyState(TimeCut timeCut)
    {
        transform.position = timeCut.position;
        transform.rotation = timeCut.rotation;

        myRigidbody.velocity = timeCut.velocity;
        myRigidbody.angularVelocity = timeCut.angularVelocity;
    }
}



