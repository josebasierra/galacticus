using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float secondsToDestroy = 10f;
    float currentSeconds = 0f;


    void OnEnable()
    {
        GetComponent<Rewindable>().OnRewind += OnRewind;
        GetComponent<Rewindable>().OnRecord += OnRecord;
    }

    void OnDisable()
    {
        GetComponent<Rewindable>().OnRewind -= OnRewind;
        GetComponent<Rewindable>().OnRecord -= OnRecord;
    }

    void FixedUpdate()
    {

       
    }

    void OnRewind(float time)
    {
        currentSeconds -= Time.fixedDeltaTime;
    }

    void OnRecord()
    {
        if (currentSeconds >= secondsToDestroy)
        {
            GameManager.Instance().Destroy(this.gameObject);
        }
        currentSeconds += Time.fixedDeltaTime;
    }
}
