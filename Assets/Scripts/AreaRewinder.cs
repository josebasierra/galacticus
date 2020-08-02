using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Change it to 'Global' rewinder
public class AreaRewinder : MonoBehaviour
{
    public float radius;

    bool isActivated = false;


    void FixedUpdate()
    {
        if (isActivated)
        {
            RewindArea();
        }
    }


    public void Activate(bool value)
    {
        isActivated = value;
    }


    void RewindArea()
    {
        var colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in colliders)
        {
            var rewindable = collider.GetComponent<Rewindable>();
            if (rewindable != null)
            {
                rewindable.StartRewind(1.5f);
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
