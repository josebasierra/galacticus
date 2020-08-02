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


    public void RewindableDestroy()
    {
        isDestroyed = true;
        timeOfDestruction = Time.fixedTime;

        //TODO: Check components
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }


    void Reactivate()
    {
        isDestroyed = false;

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }


    void OnRewind(float time)
    {
        if (CompareTag("Bullet"))
        {
            Debug.Log("Rewind time: " + time.ToString());
            Debug.Log("Time of destruction: " + timeOfDestruction.ToString());
        }
        

        if (isDestroyed && timeOfDestruction == time)
        {
            Reactivate();
        }
        else if(isDestroyed && time - timeOfDestruction > rewindable.GetMaximumRewindSeconds())
        {
            Destroy(this.gameObject);
        }
    }





    void Desactivate()
    {
        isDestroyed = true;
        timeOfDestruction = Time.fixedTime;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
