using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct RewindableRigidbodyData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity;


    public RewindableRigidbodyData(Transform transform, Rigidbody rigidbody)
    {
        position = transform.position;
        rotation = transform.rotation;
        velocity = rigidbody.velocity;
        angularVelocity = rigidbody.angularVelocity;
    }
}

//TODO: Separation of rigidbody and transform
public class RigidbodyRewinder : IRewindableData<RewindableRigidbodyData>
{
    Rigidbody rigidbody;
    Transform transform;

    BasicDataRewinder<RewindableRigidbodyData> rewinder;


    public RigidbodyRewinder(Rewindable _rewindable, Transform _transform, Rigidbody _rigidbody)
    {
        transform = _transform;
        rigidbody = _rigidbody;

        rewinder = new BasicDataRewinder<RewindableRigidbodyData>(_rewindable, this);
    }


    public RewindableRigidbodyData GetRewindableData()
    {
        return new RewindableRigidbodyData(transform, rigidbody);
    }


    public void SetRewindableData(RewindableRigidbodyData data)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;

        rigidbody.velocity = data.velocity;
        rigidbody.angularVelocity = data.angularVelocity;
    }
}
