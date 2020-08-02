using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rewindable : MonoBehaviour
{
    public struct TimeCut
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;

        public TimeCut(Transform transform, Rigidbody rigidbody)
        {
            position = transform.position;
            rotation = transform.rotation;
            velocity = rigidbody.velocity;
            angularVelocity = rigidbody.angularVelocity;
        }
    }


    [SerializeField] float maximumRewindSeconds = 10f;
    [SerializeField] Material rewindMaterial;

    Rigidbody myRigidbody;
    MeshRenderer meshRenderer;
    Material defaultMaterial;

    List<TimeCut> timeCuts;

    bool isRewinding = false;
    float rewindSeconds = 0;
    float currentRewindedSeconds = 0;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;

        timeCuts = new List<TimeCut>();
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
        meshRenderer.sharedMaterial = rewindMaterial;

        isRewinding = true;
        rewindSeconds = Mathf.Max(Time.fixedDeltaTime, time);
        currentRewindedSeconds = 0;
    }


    public void StopRewind()
    {
        meshRenderer.sharedMaterial = defaultMaterial;

        isRewinding = false;
    }


    void Rewind()
    {
        if (timeCuts.Count <= 0 || (currentRewindedSeconds > rewindSeconds))
        {
            StopRewind();
            Record();
            return;
        }

        var timeCut = timeCuts[timeCuts.Count - 1];
        ApplyState(timeCut);
        timeCuts.RemoveAt(timeCuts.Count - 1);

        currentRewindedSeconds += Time.fixedDeltaTime;
    }


    void Record()
    {
        int maximumTimeCuts = (int)(maximumRewindSeconds / Time.fixedDeltaTime);
        if (timeCuts.Count > maximumTimeCuts)
        {
            timeCuts.RemoveAt(0);
        }

        timeCuts.Add(new TimeCut(transform, myRigidbody));
    }


    void ApplyState(TimeCut timeCut)
    {
        transform.position = timeCut.position;
        transform.rotation = timeCut.rotation;

        myRigidbody.velocity = timeCut.velocity;
        myRigidbody.angularVelocity = timeCut.angularVelocity;
    }
}



