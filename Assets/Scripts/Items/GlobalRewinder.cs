using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalRewinder : MonoBehaviour, IItem
{
    bool isActivated = false;
    Rewindable rewindable;


    void OnEnable()
    {
        if (rewindable == null) rewindable = GetComponent<Rewindable>();

        rewindable.OnRewind += OnRewind;
    }

    void OnDisable()
    {
        rewindable.OnRewind -= OnRewind;
    }


    void FixedUpdate()
    {
        if (isActivated)
        {
            //TODO: check distance
            foreach (var rewindable in Rewindable.GetInstances())
            {
                rewindable.StartRewind(0.1f);
            }
        }
    }


    public void Activate(bool value)
    {
        isActivated = value;
    }


    void OnRewind(float time)
    {
        GameManager.Instance().SetCurrentLevelTime(GameManager.Instance().GetCurrentLevelTime() - 2*Time.fixedDeltaTime);
    }


    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
}
