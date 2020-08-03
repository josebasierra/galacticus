using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float secondsToDestroy = 10f;
    float currentSeconds = 0f;

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


    void OnRewind(float time)
    {
        if (rewindable.IsDestroyed()) return;

        currentSeconds -= Time.fixedDeltaTime;
    }


    void OnRecord()
    {
        if (rewindable.IsDestroyed()) return;

        if (currentSeconds >= secondsToDestroy)
        {
            GameManager.Instance().CustomDestroy(this.gameObject);
        }
        currentSeconds += Time.fixedDeltaTime;
    }
}
