using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GlobalRewinder : MonoBehaviour, IItem
{
    [SerializeField] float energyConsumptionPerSecond;

    bool isActivated = false;
    Rewindable rewindable;
    Energy energy;

    //postprocessing effects
    SplitToning splitToning;
    LensDistortion lensDistortion;



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


    private void Start()
    {
        var postProcessing = GameObject.Find("PostProcessing");

        if (postProcessing != null)
        {
            Volume volume = postProcessing.GetComponent<Volume>();
            if (volume != null)
            {
                volume.profile.TryGet(out splitToning);
                volume.profile.TryGet(out lensDistortion);
            }
        }

        energy = GetComponentInParent<Energy>();
    }


    void FixedUpdate()
    {
        if (isActivated)
        {
            bool sufficientEnergy = energy == null || energy.Consume(energyConsumptionPerSecond * Time.fixedDeltaTime);
            if (sufficientEnergy)
            {
                //TODO: check distance
                foreach (var rewindable in Rewindable.GetInstances())
                {
                    rewindable.StartRewind(Rewindable.MIN_REWIND_TIME);
                }
            }
        }
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


    void OnRewind(float time)
    {
        GameManager.Instance().SetCurrentLevelTime(GameManager.Instance().GetCurrentLevelTime() - 2*Time.fixedDeltaTime);
    }


    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
}
