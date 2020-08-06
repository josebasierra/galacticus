using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GlobalRewinder : MonoBehaviour, IItem
{
    bool isActivated = false;
    Rewindable rewindable;

    SplitToning splitToning;
    LensDistortion lensDistortion;

    private void Start()
    {
        var pp = GameObject.Find("PostProcessing")?.GetComponent<Volume>();

        if (pp != null) 
        {
            pp.profile.TryGet(out splitToning);
            pp.profile.TryGet(out lensDistortion);
        }
  
    }

    public void Activate(bool value)
    {
        if (value && !isActivated)
        {
            splitToning.active = true;
            //lensDistortion.active = true;
        }
        else if(!value && isActivated)
        {
            splitToning.active = false;
            //lensDistortion.active = false;
        }
        isActivated = value;
    }


    public bool IsActivated()
    {
        return isActivated;
    }


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
                rewindable.StartRewind(Rewindable.MIN_REWIND_TIME);
            }
        }
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
