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
        isDestroyed = true;
        timeOfDestruction = Time.fixedTime;

        //TODO: Check components
        GetComponent<MeshRenderer>().enabled = false;
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

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }


    void OnRewind(float timeOfRewind)
    {
        if (isDestroyed && timeOfDestruction == timeOfRewind)
        {
            Reactivate();
        }
    }
}
