using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableDestruction : MonoBehaviour
{
    bool isDestroyed = false;
    float timeOfDestruction = -1;

    Rewindable rewindable;


    void OnEnable()
    {
        rewindable = GetComponent<Rewindable>();
        rewindable.OnRewind += OnRewind;
    }


    void OnDisable()
    {
        rewindable.OnRewind -= OnRewind;
    }


    void FixedUpdate()
    {
        if (isDestroyed && Time.fixedTime - timeOfDestruction > rewindable.GetMaximumRewindSeconds())
        {
            Destroy(this.gameObject);
        }
    }


    public void RewindableDestroy()
    {
        if (rewindable.IsRewinding() || isDestroyed) return;

        isDestroyed = true;
        timeOfDestruction = Time.fixedTime;

        //TODO: Check components

        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach(var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }


    public bool IsDestroyed()
    {
        return isDestroyed;
    }


    void Reactivate()
    {
        isDestroyed = false;

        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }


    void OnRewind(float timeOfRewind)
    {
        //if (CompareTag("Bullet"))
        //{
        //    Debug.Log("Time of rewind:" + timeOfRewind.ToString());
        //    Debug.Log("Time of destruction" + timeOfDestruction.ToString());
        //}

        if (isDestroyed && timeOfDestruction == timeOfRewind)
        {
            Reactivate();
        }
    }
}
