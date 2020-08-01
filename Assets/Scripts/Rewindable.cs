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

    public bool isRewinding = false;
    public float maximumRewindSeconds = 10f;

    Rigidbody myRigidbody;
    List<TimeCut> timeCuts;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        timeCuts = new List<TimeCut>();
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

    public bool IsRewinding()
    {
        return isRewinding;
    }

    public void SetIsRewinding(bool value)
    {
        isRewinding = value;
        if (isRewinding)
        {
            myRigidbody.isKinematic = true;
        }
        else
        {
            myRigidbody.isKinematic = false;
        }
    }


    void Rewind()
    {
        if (timeCuts.Count <= 0)
        {
            SetIsRewinding(false);
            return;
        }

        var timeCut = timeCuts[timeCuts.Count - 1];
        ApplyState(timeCut);
        timeCuts.RemoveAt(timeCuts.Count - 1);
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



