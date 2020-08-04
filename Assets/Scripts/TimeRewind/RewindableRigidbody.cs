using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableRigidbody
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

    Rigidbody rigidbody;
    Transform transform;

    Rewindable rewindable;
    LimitedStack<TimeCut> timeCuts;


    public RewindableRigidbody(Rewindable _rewindable, Transform _transform, Rigidbody _rigidbody)
    {
        rewindable = _rewindable;
        transform = _transform;
        rigidbody = _rigidbody;

        timeCuts = new LimitedStack<TimeCut>(rewindable.MaxCapacity());

        rewindable.OnRewind += OnRewind;
        rewindable.OnRecord += OnRecord;
    }

    ~RewindableRigidbody()
    {
        rewindable.OnRewind -= OnRewind;
        rewindable.OnRecord -= OnRecord;
    }


    void OnRewind(float time)
    {
        var timeCut = timeCuts.Top();
        ApplyState(timeCut);
        timeCuts.Pop();
    }


    void OnRecord()
    {
        timeCuts.Push(new TimeCut(transform, rigidbody, Time.fixedTime));
    }


    void ApplyState(TimeCut timeCut)
    {
        transform.position = timeCut.position;
        transform.rotation = timeCut.rotation;

        rigidbody.velocity = timeCut.velocity;
        rigidbody.angularVelocity = timeCut.angularVelocity;
    }
}
