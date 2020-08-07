using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour, IItem
{
    [SerializeField] float range;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    [Header("Audio")]
    [SerializeField] AudioClip laserSound;

    LineRenderer lineRenderer;
    public bool isActivated = false;

    AudioComponent audioComponent;


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

        audioComponent = GetComponent<AudioComponent>();
    }


    void FixedUpdate()
    {
        if (isActivated)
        {
            EmitLaser();
            if (audioComponent != null) audioComponent.Play(laserSound);
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

        if (Physics.Raycast(startPoint.position, laserDirection, out hit, range))
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


    //time rewind....

    Rewindable rewindable;
    LimitedStack<bool> isActivatedRegister;

    void OnEnable()
    {
        if (rewindable == null)
        {
            rewindable = GetComponentInParent<Rewindable>();
            isActivatedRegister = new LimitedStack<bool>(rewindable.MaxCapacity());
        }
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
        Activate(isActivatedRegister.Top());
        isActivatedRegister.Pop();

    }

    void OnRecord()
    {
        isActivatedRegister.Push(isActivated);

    }

}
