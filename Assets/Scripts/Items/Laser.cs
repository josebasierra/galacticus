using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour, IItem, IRewindableData<bool>
{
    [SerializeField] float range;
    [SerializeField] float energyConsumptionPerSecond;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    LineRenderer lineRenderer;
    public bool isActivated = false;
    Energy energy;

    [Header("Audio")]
    [SerializeField] AudioClip laserSound;
    AudioComponent audioComponent;

    BasicDataRewinder<bool> rewinder;


    public void Activate(bool value)
    {
        isActivated = value;
        lineRenderer.enabled = value;
    }


    public bool IsActivated()
    {
        return isActivated;
    }


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;

        lineRenderer.enabled = isActivated;

        energy = GetComponentInParent<Energy>();

        audioComponent = GetComponent<AudioComponent>();

        rewinder = new BasicDataRewinder<bool>(GetComponentInParent<Rewindable>(), this);
    }


    void FixedUpdate()
    {
        if (isActivated)
        {
            bool sufficientEnergy = energy == null || energy.Consume(energyConsumptionPerSecond * Time.fixedDeltaTime);
            if (sufficientEnergy)
            {
                EmitLaser();
                if (audioComponent != null) audioComponent.Play(laserSound);
            }
        }

        else
        {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, startPoint.position);
            if (audioComponent != null) audioComponent.Stop(laserSound);
        }
    }


    void EmitLaser()
    {
        lineRenderer.SetPosition(0, startPoint.position);
        Vector3 laserDirection = (endPoint.position - startPoint.position).normalized;
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Default");
        if (Physics.Raycast(startPoint.position, laserDirection, out hit, range, mask))
        {
            HitEffect(hit);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, startPoint.position + range * laserDirection);
        }
    }


    protected virtual void HitEffect(RaycastHit hit)
    {

    }


    public bool GetRewindableData()
    {
        return isActivated;
    }

    public void SetRewindableData(bool data)
    {
        Activate(data);
    }
}
