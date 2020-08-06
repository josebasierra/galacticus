using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    [SerializeField] float maximumTimeInDeadState = 3f;

    Rewindable rewindable;
    Health health;

    float timeInDeadState = 0;

    void Start()
    {
        rewindable = GetComponent<Rewindable>();
        health = GetComponent<Health>();

        rewindable.OnRewind += OnRewind;
    }


    void OnDestroy()
    {
        rewindable.OnRewind -= OnRewind;
    }


    void FixedUpdate()
    {
        if (health.IsDead())
        {  
            timeInDeadState += Time.fixedDeltaTime;
            if (timeInDeadState > maximumTimeInDeadState) GameManager.Instance().Defeat();
        }
    }


    void OnRewind(float time)
    {
        if (health.IsDead()) timeInDeadState -= 2 * Time.fixedDeltaTime;
    }


}
