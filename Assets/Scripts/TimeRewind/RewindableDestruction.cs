using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RewindableDestruction : MonoBehaviour
{
    bool isDestroyed = false;
    float timeOfDestruction = -1;
    float timeDestroyed = 0;

    public Action OnRewindableDestroy, OnRewindableReactivate;

    Rewindable rewindable;


    void OnEnable()
    {
        rewindable = GetComponent<Rewindable>();
        rewindable.OnRewind += OnRewind;
        rewindable.OnRecord += OnRecord;
    }


    void OnDisable()
    {
        rewindable.OnRewind -= OnRewind;
        rewindable.OnRecord -= OnRecord;
    }


    void FixedUpdate()
    {

    }


    public bool IsDestroyed()
    {
        return isDestroyed;
    }


    public void RewindableDestroy()
    {
        if (rewindable.IsRewinding() || isDestroyed) return;

        isDestroyed = true;
        timeOfDestruction = Time.fixedTime;

        OnRewindableDestroy?.Invoke();

        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach(var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }

        var colliders = GetComponentsInChildren<Collider>();
        foreach(var collider in colliders)
        {
            collider.enabled = false;
        }

        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }




    void Reactivate()
    {
        isDestroyed = false;
        timeDestroyed = 0;

        OnRewindableReactivate?.Invoke();

        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }

        var colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }

        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }


    void OnRewind(float timeOfRewind)
    {
        //if (CompareTag("Bullet"))
        //{
        //    Debug.Log("Time of rewind:" + timeOfRewind.ToString());
        //    Debug.Log("Time of destruction" + timeOfDestruction.ToString());
        //}
        if (isDestroyed)
        {
            timeDestroyed -= Time.fixedDeltaTime;
        }
        if (isDestroyed && timeOfDestruction == timeOfRewind)
        {
            Reactivate();
        }
    }

    void OnRecord()
    {
        if (isDestroyed)
        {
            timeDestroyed += Time.fixedDeltaTime;
            if (timeDestroyed > rewindable.GetMaximumRewindSeconds())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
